using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericQueryHandler
    {
        protected readonly Repository _repository;

        protected AbstractGenericQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public virtual async Task<List<T>> GetWithCustomSearchAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier,
            ILogger logger,
            [CallerMemberName] string methodName = "") where T : class
        {
            var query = _repository.Export<T>(logger);

            if (queryModifier != null)
                query = queryModifier(query);

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Executing query: {query.ToQueryString()} for entity {typeof(T).Name}");

            var result = await query.ToListAsync();

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Query executed. Result count: {result.Count}");

            return result;
        }

        public virtual async Task<T?> GetBySpecificPropertySingularAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier,
            ILogger logger,
            [CallerMemberName] string methodName = "") where T : class
        {
            var query = _repository.Export<T>(logger);

            if (queryModifier != null)
                query = queryModifier(query);

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Executing singular query: {query.ToQueryString()} for entity {typeof(T).Name}");

            var result = await query.FirstOrDefaultAsync();

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Query executed. Result is null: {result == null}");

            return result;
        }

        public virtual async Task<int> GetCountBySpecificPropertyAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier,
            ILogger logger,
            [CallerMemberName] string methodName = "") where T : class
        {
            var query = _repository.Export<T>(logger);

            if (queryModifier != null)
                query = queryModifier(query);

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Executing count query: {query.ToQueryString()} for entity {typeof(T).Name}");

            var count = await query.CountAsync();

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Query executed. Count: {count}");

            return count;
        }

        public virtual async Task<bool> ExistsBySpecificPropertyAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier,
            ILogger logger,
            [CallerMemberName] string methodName = "") where T : class
        {
            var query = _repository.Export<T>(logger);

            if (queryModifier != null)
                query = queryModifier(query);

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Executing exists query: {query.ToQueryString()} for entity {typeof(T).Name}");

            var exists = await query.AnyAsync();

            logger.LogInformation($"{this.GetType().Name}.{methodName}: Query executed. Exists: {exists}");

            return exists;
        }

        public virtual async Task<List<T>> ReloadEntityModuleBySpecificProperty<T>(
            int startInterval,
            int endInterval,
            ILogger logger,
            [CallerMemberName] string methodName = "") where T : class
        {
            var query = _repository.Export<T>(logger);

            logger.LogInformation("{Handler}.{Method}: Reload started for {Entity}. Interval: {Start}-{End}",
                this.GetType().Name, methodName, typeof(T).Name, startInterval, endInterval);

            List<T> result;

            if (typeof(T) == typeof(Post))
            {
                result = await query.Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(p => new Post
                    {
                        ContentItemId = ((Post)(object)p).ContentItemId,
                        Title = ((Post)(object)p).Title,
                        Content = ((Post)(object)p).Content,
                        LikeCount = ((Post)(object)p).LikeCount,
                        CreatedAt = ((Post)(object)p).CreatedAt,
                        UpdatedAt = ((Post)(object)p).UpdatedAt,
                        ActorId = ((Post)(object)p).ActorId,
                        Actor = ((Post)(object)p).Actor,
                    })
                    .Cast<T>()
                    .ToListAsync();

                logger.LogInformation("{Handler}.{Method}: Post reload completed. Retrieved {Count} items.",
                    this.GetType().Name, methodName, result.Count);
            }
            else if (typeof(T) == typeof(Entry))
            {
                result = await query.Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(e => new Entry
                    {
                        ContentItemId = ((Entry)(object)e).ContentItemId,
                        Content = ((Entry)(object)e).Content,
                        LikeCount = ((Entry)(object)e).LikeCount,
                        CreatedAt = ((Entry)(object)e).CreatedAt,
                        UpdatedAt = ((Entry)(object)e).UpdatedAt,
                        ActorId = ((Entry)(object)e).ActorId,
                        Actor = ((Entry)(object)e).Actor,
                    })
                    .Cast<T>()
                    .ToListAsync();

                logger.LogInformation("{Handler}.{Method}: Entry reload completed. Retrieved {Count} items.",
                    this.GetType().Name, methodName, result.Count);
            }
            else if (typeof(T) == typeof(Like))
            {
                result = await query.Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(l => new Like
                    {
                        LikeId = ((Like)(object)l).LikeId,
                        ActorId = ((Like)(object)l).ActorId,
                        Actor = ((Like)(object)l).Actor,
                        ContentItemId = ((Like)(object)l).ContentItemId,
                        ContentItem = ((Like)(object)l).ContentItem,
                        ReactionType = ((Like)(object)l).ReactionType,
                        CreatedAt = ((Like)(object)l).CreatedAt,
                    })
                    .Cast<T>()
                    .ToListAsync();

                logger.LogInformation("{Handler}.{Method}: Like reload completed. Retrieved {Count} items.",
                    this.GetType().Name, methodName, result.Count);
            }
            else if (typeof(T) == typeof(Follow))
            {
                result = await query.Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(f => new Follow
                    {
                        FollowId = ((Follow)(object)f).FollowId,
                        FollowerActorId = ((Follow)(object)f).FollowerActorId,
                        FollowerActor = ((Follow)(object)f).FollowerActor,
                        FollowedActorId = ((Follow)(object)f).FollowedActorId,
                        FollowedActor = ((Follow)(object)f).FollowedActor,
                        CreatedAt = ((Follow)(object)f).CreatedAt,
                    })
                    .Cast<T>()
                    .ToListAsync();

                logger.LogInformation("{Handler}.{Method}: Follow reload completed. Retrieved {Count} items.",
                    this.GetType().Name, methodName, result.Count);
            }
            else if (typeof(T) == typeof(Notifications))
            {
                result = await query.Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(n => new Notifications
                    {
                        NotificationId = ((Notifications)(object)n).NotificationId,
                        SenderActor = ((Notifications)(object)n).SenderActor,
                        SenderActorId = ((Notifications)(object)n).SenderActorId,
                        Message = ((Notifications)(object)n).Message,
                        IsRead = ((Notifications)(object)n).IsRead,
                        CreatedAt = ((Notifications)(object)n).CreatedAt,
                    })
                    .Cast<T>()
                    .ToListAsync();

                logger.LogInformation("{Handler}.{Method}: Notifications reload completed. Retrieved {Count} items.",
                    this.GetType().Name, methodName, result.Count);
            }
            else if (typeof(T) == typeof(BotActivity))
            {
                result = await query.Skip(startInterval)
                    .Take(endInterval - startInterval)
                    .Select(b => new BotActivity
                    {
                        BotActivityId = ((BotActivity)(object)b).BotActivityId,
                        AdditionalId = ((BotActivity)(object)b).AdditionalId,
                        AdditionalIdType = ((BotActivity)(object)b).AdditionalIdType,
                        IsRead = ((BotActivity)(object)b).IsRead,
                        Message = ((BotActivity)(object)b).Message,
                        BotId = ((BotActivity)(object)b).BotId,
                        Bot = ((BotActivity)(object)b).Bot,
                        CreatedAt = ((BotActivity)(object)b).CreatedAt,
                    })
                    .Cast<T>()
                    .ToListAsync();

                logger.LogInformation("{Handler}.{Method}: BotActivity reload completed. Retrieved {Count} items.",
                    this.GetType().Name, methodName, result.Count);
            }
            else
            {
                logger.LogWarning("{Handler}.{Method}: ReloadEntityModuleBySpecificProperty not implemented for {Entity}.",
                    this.GetType().Name, methodName, typeof(T).Name);
                return new List<T>();
            }

            return result;
        }

    }
}
