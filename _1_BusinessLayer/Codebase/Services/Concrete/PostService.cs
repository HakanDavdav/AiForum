using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.InputDtos;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.OutputDtos;
using _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos;
using _1_BusinessLayer.Codebase.Mappers;
using _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents;
using _1_BusinessLayer.Concrete.Services._Concrete;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Cqrs;
using _2_DataAccessLayer.Concrete.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _1_BusinessLayer.Codebase.Services.Concrete
{
    public class PostService
    {
        private readonly AbstractGenericQueryHandler _queryHandler;
        private readonly AbstractGenericCommandHandler _commandHandler;
        private readonly IValidator<CreateEditPostDto> _createEditPostDtoValidator;
        private readonly ILogger<PostService> _logger;

        public PostService(AbstractGenericCommandHandler commandHandler, AbstractGenericQueryHandler queryHandler,
            ILogger<PostService> logger, IValidator<CreateEditPostDto> createEditPostDtoValidator)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _createEditPostDtoValidator = createEditPostDtoValidator;
            _logger = logger;
        }

        public async Task<IdentityResult> CreatePost(Guid userId, CreateEditPostDto createEditPostDto)
        {
            _logger.LogInformation("CreatePost started for UserId={UserId}", userId);

            var validationResult = await _createEditPostDtoValidator.ValidateAsync(createEditPostDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("CreatePost validation failed for UserId={UserId}: {Errors}", userId, errors);
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, errors));
            }

            var user = await _queryHandler.GetBySpecificPropertySingularAsync<User>(q => q.Where(u => u.ActorId == userId));
            if (user == null)
            {
                _logger.LogWarning("CreatePost aborted: user not found. UserId={UserId}", userId);
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "User not found"));
            }

            var post = new Post
            {
                ContentItemId = Guid.NewGuid(),
                ActorId = userId,
                Title = createEditPostDto.Title,
                Content = createEditPostDto.Content,
                TopicTypes = createEditPostDto.TopicTypes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _commandHandler.ManuallyInsertAsync(post);
            user.PostCount += 1;

            // Prepare event
            var postEvent = new PostCreatedEvent
            {
                CreatedPostId = post.ContentItemId,
                CreatorActorId = user.ActorId,
                CreatedAt = post.CreatedAt
            };

            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(PostCreatedEvent),
                Payload = JsonSerializer.Serialize(postEvent),
                ProcessedOn = null,
                RetryCount = 0
            };

            await _commandHandler.ManuallyInsertAsync(outbox);
            await _commandHandler.SaveChangesAsync();

            _logger.LogInformation("CreatePost succeeded for UserId={UserId}, PostId={PostId}", userId, post.ContentItemId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeletePost(Guid userId, Guid postId)
        {
            var post = await _queryHandler.GetBySpecificPropertySingularAsync<Post>(q => q.Where(p => p.ContentItemId == postId && p.ActorId == userId).Include(p => p.Actor));
            if (post == null)
            {
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Post not found or user unauthorized"));
            }
            if (post.Actor == null)
            {
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Post's actor not found"));
            }
            post.Actor.PostCount -= 1;
            await _commandHandler.DeleteAsync<Post>(post);
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;

        }

        public async Task<IdentityResult> EditPost(Guid userId, Guid postId ,CreateEditPostDto createEditPostDto)
        {
            var validationResult = await _createEditPostDtoValidator.ValidateAsync(createEditPostDto);
            if(!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return IdentityResult.Failed(new AppError(ErrorType.ValidationError, errors));
            }
            var post = await _queryHandler.GetBySpecificPropertySingularAsync<Post>(q => q.Where(p => p.ContentItemId == postId && p.ActorId == userId).Include(p => p.Actor));
            if (post == null)
            {
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Post not found or user unauthorized"));
            }
            if (post.Actor == null)
            {
                return IdentityResult.Failed(new AppError(ErrorType.NotFound, "Post's actor not found"));
            }
            post.Content = createEditPostDto.Content;
            post.Title = createEditPostDto.Title;
            post.TopicTypes = createEditPostDto.TopicTypes;
            post.UpdatedAt = DateTime.UtcNow;
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<ObjectIdentityResult<List<EntryDto>>> LoadPostEntryModules(Guid postId,ClaimsPrincipal claims ,int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var entries = await _queryHandler.ReloadEntityModuleBySpecificProperty<Entry>(q => q.Where(e => e.ParentContentId == postId),startInterval, endInterval);
            List<EntryDto> entryDtos = new List<EntryDto>();
            foreach (var entry in entries)
            {
                entryDtos.Add(entry.Entry_To_ExtendedEntryDto());
            }
            return new ObjectIdentityResult<List<EntryDto>>(entryDtos);

        }

        public async Task<ObjectIdentityResult<List<LikeDto>> LoadPostLikeModules(Guid postId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var likes = await _queryHandler.ReloadEntityModuleBySpecificProperty<Like>(q => q.Where(l => l.ContentItemId == postId), startInterval, endInterval);
            List<LikeDto> likeDtos = new List<LikeDto>();
            foreach (var like in likes)
            {
                likeDtos.Add(like.Like_To_ExtendedLikeDto());
            }
            return ObjectIdentityResult.Success<List<LikeDto>>(likeDtos);
        }

        public async Task<IdentityResult> GetPost(Guid postId)
        {
            var post = await _queryHandler.GetBySpecificPropertySingularAsync<Post>(q => q.Where(p => p.ContentItemId == postId));           
            var postDto = post.Post_To_ExtendedPostDto();
            return ObjectIdentityResult.Success

        }
    }
}
