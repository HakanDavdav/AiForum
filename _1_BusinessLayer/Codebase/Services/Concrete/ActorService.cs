using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos;
using _1_BusinessLayer.Codebase.Events.Concrete.SocialEvents;
using _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _1_BusinessLayer.Concrete.Services._Concrete
{
    public class ActorService
    {
        AbstractGenericCommandHandler _commandHandler;
        AbstractGenericQueryHandler _queryHandler;
        IValidator<CreateEditUserProfileDto> _createEditActorValidator;
        IValidator<CreateEditBotProfileDto> _createEditBotValidator;
        private readonly ILogger<ActorService> _logger;

        public ActorService(AbstractGenericCommandHandler commandHandler, AbstractGenericQueryHandler queryHandler,
            IValidator<CreateEditUserProfileDto> createEditUserProfileDtoValidator, IValidator<CreateEditBotProfileDto> createEditBotProfileDtoValidator,
            ILogger<ActorService> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _createEditActorValidator = createEditUserProfileDtoValidator;
            _createEditBotValidator = createEditBotProfileDtoValidator;
            _logger = logger;
        }

        public async Task<IdentityResult> InitializeUserProfile(Guid userId, CreateEditUserProfileDto createEditUserProfileDto)
        {
            _logger.LogInformation("InitializeUserProfile started for UserId={UserId}", userId);

            var validationResult = await _createEditActorValidator.ValidateAsync(createEditUserProfileDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                _logger.LogWarning("InitializeUserProfile validation failed for UserId={UserId}: {Errors}", userId, string.Join("; ", errors));
                return IdentityResult.Failed(errors.Select(e => new IdentityError { Code = "ValidationError", Description = e }).ToArray());
            }
            var user = await _queryHandler.GetBySpecificPropertySingularAsync<User>(x => x.Where(u => u.ActorId == userId).Include(u => u.UserSettings));
            if (user == null)
            {
                _logger.LogWarning("InitializeUserProfile aborted: user not found. UserId={UserId}", userId);
                return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found" });
            }
            if (user.UserSettings == null)
            {
                _logger.LogWarning("InitializeUserProfile aborted: user settings not found. UserId={UserId}", userId);
                return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User settings not found" });
            }

            try
            {
                user.UserSettings.EntryPerPage = createEditUserProfileDto.EntryPerPage.Value;
                user.UserSettings.PostPerPage = createEditUserProfileDto.PostPerPage.Value;
                user.UserSettings.Theme = createEditUserProfileDto.Theme.Value;
                user.UserSettings.SocialEmailPreference = createEditUserProfileDto.SocialEmailPreference.Value;
                user.UserSettings.SocialNotificationPreference = createEditUserProfileDto.SocialNotificationPreference.Value;
                user.ProfileName = createEditUserProfileDto.ProfileName;
                user.ImageUrl = createEditUserProfileDto.ImageUrl;
                user.Bio = createEditUserProfileDto.Bio;
                user.Interests = createEditUserProfileDto.Interests.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IntitializeUserProfile cannot executed");
                throw;
            }
            await _commandHandler.SaveChangesAsync();

            _logger.LogInformation("InitializeUserProfile succeeded for UserId={UserId}", userId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> CreateBot(Guid botId, CreateEditBotProfileDto createEditBotProfileDto)
        {
            _logger.LogInformation("CreateBot started for BotId={BotId}", botId);

            var validationResult = await _createEditBotValidator.ValidateAsync(createEditBotProfileDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                _logger.LogWarning("CreateBot validation failed for BotId={BotId}: {Errors}", botId, string.Join("; ", errors));
                return IdentityResult.Failed(errors.Select(e => new IdentityError { Code = "ValidationError", Description = e }).ToArray());
            }

            var botCreatedEvent = new BotCreatedEvent
            {
                ActorId = botId,
                CreatedAt = DateTime.UtcNow,
                ProfileName = createEditBotProfileDto.ProfileName,
                ImageUrl = createEditBotProfileDto.ImageUrl,
                Bio = createEditBotProfileDto.Bio,
                Interests = createEditBotProfileDto.Interests,
                AutoInterests = createEditBotProfileDto.AutoInterests,
                AutoBio = createEditBotProfileDto.AutoBio,
                BotPersonality = createEditBotProfileDto.BotPersonality,
                Instructions = createEditBotProfileDto.Instructions,
                DailyBotOperationCount = createEditBotProfileDto.DailyBotOperationCount,
                BotMode = createEditBotProfileDto.BotMode
            };

            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(BotCreatedEvent),
                Payload = JsonSerializer.Serialize(botCreatedEvent),
                ProcessedOn = null,
                RetryCount = 0
            };

            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();

            _logger.LogInformation("CreateBot succeeded for BotId={BotId}", botId);
            return IdentityResult.Success;
        }

        public async Task<Actor?> GetActorByIdAsync(Guid actorId)
        {
            return await _queryHandler.ReloadEntityModuleBySpecificProperty
        }
    }
}
