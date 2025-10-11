using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events.Concrete.SocialEvents;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Codebase.Services.Concrete
{
    public class LikeService
    {
        AbstractGenericCommandHandler _commandHandler;
        AbstractGenericQueryHandler _queryHandler;
        public LikeService(AbstractGenericCommandHandler commandHandler, AbstractGenericQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;

        }

        public async Task<IdentityResult> Like(Guid likerActorId, Guid contentItemId, ReactionType reactionType )
        {
            var like = new Like
            {
                LikeId = Guid.NewGuid(),
                ActorId = likerActorId,
                ContentItemId = contentItemId,
                ReactionType = reactionType,
                CreatedAt = DateTime.UtcNow
            };           
            await _commandHandler.ManuallyInsertAsync<Like>(like);
            var likedEvent = new LikedEvent
            {
                ActorId = likerActorId,
                contentItemId = contentItemId,
                ReactionType = reactionType,
                CreatedAt = like.CreatedAt,              
            };
            var outbox = new OutboxMessage
            {
                OutboxMessageId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = nameof(LikedEvent),
                Payload = JsonSerializer.Serialize(likedEvent),
                ProcessedOn = null,
                RetryCount = 0
            };
            await _commandHandler.ManuallyInsertAsync<OutboxMessage>(outbox);
            await _commandHandler.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}
