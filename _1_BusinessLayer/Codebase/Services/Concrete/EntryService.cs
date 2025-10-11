using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.InputDtos;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.OutputDtos;
using _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos;
using _1_BusinessLayer.Codebase.Mappers;
using _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace _1_BusinessLayer.Codebase.Services.Concrete
{
    public class EntryService
    {
        AbstractGenericCommandHandler _commandHandler;
        AbstractGenericQueryHandler _queryHandler;
        IValidator<CreateEditEntryDto> _createEditEntryDtoValidator;
        private readonly ILogger<EntryService> _logger;

        public EntryService(AbstractGenericCommandHandler commandHandler, AbstractGenericQueryHandler queryHandler,
            IValidator<CreateEditEntryDto> createEditEntryDtoValidator, ILogger<EntryService> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            this._createEditEntryDtoValidator = createEditEntryDtoValidator;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateEntryAsync(Guid userId,Guid contentItemId ,CreateEditEntryDto createEditEntryDto)
        {
            _logger.LogInformation("CreateEntryAsync started for UserId={UserId}", userId);

            var validationResult = await _createEditEntryDtoValidator.ValidateAsync(createEditEntryDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("CreateEntryAsync validation failed for UserId={UserId}: {Errors}", userId, errors);
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, errors));
            }
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<User>(x => x.Where(a => a.ActorId == userId));
            if (user == null)
            {
                _logger.LogWarning("CreateEntryAsync aborted: user not found. UserId={UserId}", userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found"));
            }
            var entry = new Entry
            {
                ContentItemId = Guid.NewGuid(),
                ParentContentId = contentItemId,
                Content = createEditEntryDto.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ActorId = userId,
            };
            await _commandHandler.ManuallyInsertAsync<Entry>(entry);

            var entryCreatedEvent = new EntryCreatedEvent
            {
                ContentItemId = entry.ContentItemId,
                ActorId = userId,
                CreatedAt = entry.CreatedAt,
            };

            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(EntryCreatedEvent),
                Payload = JsonSerializer.Serialize(entryCreatedEvent),
                ProcessedOn = null,
                RetryCount = 0
            };

            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();

            _logger.LogInformation("CreateEntryAsync succeeded for UserId={UserId}, EntryId={EntryId}", userId, entry.ContentItemId);
            return IdentityResult.Success;
        }


        public async Task<IdentityResult> EditEntryAsync(Guid userId, Guid entryId, CreateEditEntryDto createEditEntryDto)
        {
            _logger.LogInformation("EditEntryAsync started for UserId={UserId}, EntryId={EntryId}", userId, entryId);

            var validationResult = await _createEditEntryDtoValidator.ValidateAsync(createEditEntryDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("EditEntryAsync validation failed for UserId={UserId}, EntryId={EntryId}: {Errors}", userId, entryId, errors);
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, errors));
            }
            var entry = await _queryHandler.GetBySpecificPropertySingularAsync<Entry>(x => x.Where(e => e.ContentItemId == entryId && e.ActorId == userId));
            if (entry == null)
            {
                _logger.LogWarning("EditEntryAsync aborted: entry not found or user unauthorized. UserId={UserId}, EntryId={EntryId}", userId, entryId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Entry not found or you do not have permission to edit it"));
            }
            entry.Content = createEditEntryDto.Content;
            entry.UpdatedAt = DateTime.UtcNow;
            await _commandHandler.SaveChangesAsync();

            _logger.LogInformation("EditEntryAsync succeeded for UserId={UserId}, EntryId={EntryId}", userId, entryId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteEntryAsync(Guid userId, Guid entryId)
        {
            _logger.LogInformation("DeleteEntryAsync started for UserId={UserId}, EntryId={EntryId}", userId, entryId);

            var entry = await _queryHandler.GetBySpecificPropertySingularAsync<Entry>(x => x.Where(e => e.ContentItemId == entryId && e.ActorId == userId));
            if (entry == null)
            {
                _logger.LogWarning("DeleteEntryAsync aborted: entry not found or user unauthorized. UserId={UserId}, EntryId={EntryId}", userId, entryId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Entry not found or you do not have permission to delete it"));
            }
            await _commandHandler.DeleteAsync<Entry>(entry);
            await _commandHandler.SaveChangesAsync();

            _logger.LogInformation("DeleteEntryAsync succeeded for UserId={UserId}, EntryId={EntryId}", userId, entryId);
            return IdentityResult.Success;
        }

        public async Task<ObjectIdentityResult<List<LikeDto>>> LoadEntryLikesModules(Guid entryId , int page)
        {
            _logger.LogInformation("LoadEntryLikeModules started for EntryId={EntryId}, Page={Page}", entryId, page);

            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var likes = await _queryHandler.ReloadEntityModuleBySpecificProperty<Like>(q => q.Where(l => l.ContentItemId == entryId), startInterval, endInterval);
            List<LikeDto> minimalLikeDtos = new List<LikeDto>();
            foreach (var like in likes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }

            _logger.LogInformation("LoadEntryLikeModules succeeded for EntryId={EntryId}. Count={Count}, Start={Start}, End={End}", entryId, minimalLikeDtos.Count, startInterval, endInterval);
            return ObjectIdentityResult<List<LikeDto>>.Succeded(minimalLikeDtos);
        }
    }
}
