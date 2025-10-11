using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _2_DataAccessLayer.Concrete.Cqrs
{
    public class GenericQueryHandler : AbstractGenericQueryHandler
    {
        public GenericQueryHandler(Repository repository, ILogger logger) : base(repository, logger)
        {
        }

        public enum IncludeVariant
        {
            FullDetails,
            Minimal
        }

        public override async Task<List<T>> GetWithCustomSearchAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            try
            {
                var query = _repository.Export<T>(_logger);
                if (queryModifier != null)
                    query = queryModifier(query);
                _logger.LogInformation("Executing query: {Query}", query.ToQueryString());
                var result = await query.ToListAsync();
                _logger.LogInformation("Query executed. Result count: {Count}", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query cannot executed");
                throw;
            }
        }

        public override async Task<T?> GetBySpecificPropertySingularAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            try
            {
                var query = _repository.Export<T>(_logger);
                if (queryModifier != null)
                    query = queryModifier(query);
                _logger.LogInformation("Executing singular query: {Query}", query.ToQueryString());
                var result = await query.FirstOrDefaultAsync();
                _logger.LogInformation("Query executed. Result is null: {IsNull}", result == null);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query cannot executed");
                throw;
            }
        }

        public override async Task<int> GetCountBySpecificPropertyAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            try
            {
                var query = _repository.Export<T>(_logger);
                if (queryModifier != null)
                    query = queryModifier(query);
                _logger.LogInformation("Executing count query: {Query}", query.ToQueryString());
                var count = await query.CountAsync();
                _logger.LogInformation("Query executed. Count: {Count}", count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query cannot executed");
                throw;
            }
        }

        public override async Task<bool> ExistsBySpecificPropertyAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            try
            {
                var query = _repository.Export<T>(_logger);
                if (queryModifier != null)
                    query = queryModifier(query);
                _logger.LogInformation("Executing exists query: {Query}", query.ToQueryString());
                var exists = await query.AnyAsync();
                _logger.LogInformation("Query executed. Exists: {Exists}", exists);
                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query cannot executed");
                throw;
            }
        }

        public override async Task<List<T>> ReloadEntityModuleBySpecificProperty<T>(
            Func<IQueryable<T>, IQueryable<T>>? filter,
            IncludeVariant includeVariant,
            int startInterval,
            int endInterval) where T : class
        {
            try
            {
                IQueryable<T> query = _repository.Export<T>(_logger);

                // Apply filter before pagination for efficiency
                if (filter is not null)
                    query = filter(query);

                var take = Math.Max(0, endInterval - startInterval);
                if (startInterval > 0)
                    query = query.Skip(startInterval);
                if (take > 0)
                    query = query.Take(take);

                _logger.LogInformation("Executing reload query: {Query}", query.ToQueryString());

                IQueryable<T> ProjectPost(IQueryable<Post> q, IncludeVariant variant)
                    => q.Select(p => new Post
                    {
                        ContentItemId = p.ContentItemId,
                        ActorId = p.ActorId,
                        Content = p.Content,
                        LikeCount = p.LikeCount,
                        EntryCount = p.EntryCount,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        Title = p.Title,
                        TopicTypes = p.TopicTypes,
                        Actor = ProjectMinimalActorStatic(p.Actor)
                    }).Cast<T>();

                IQueryable<T> ProjectEntry(IQueryable<Entry> q, IncludeVariant variant)
                    => q.Select(e => new Entry
                    {
                        ContentItemId = e.ContentItemId,
                        Content = e.Content,
                        LikeCount = e.LikeCount,
                        EntryCount = e.EntryCount,
                        CreatedAt = e.CreatedAt,
                        UpdatedAt = e.UpdatedAt,
                        ParentContent = variant == IncludeVariant.FullDetails && e.ParentContent != null
                            ? (e.ParentContent is Post
                                ? new Post { ContentItemId = e.ParentContent.ContentItemId, Title = ((Post)e.ParentContent).Title }
                                : new Post { ContentItemId = e.ParentContent.ContentItemId, Title = null })
                            : null,
                        Actor = ProjectMinimalActorStatic(e.Actor)
                    }).Cast<T>();

                IQueryable<T> ProjectLike(IQueryable<Like> q, IncludeVariant variant)
                    => q.Select(l => new Like
                    {
                        LikeId = l.LikeId,
                        ActorId = l.ActorId,
                        ContentItemId = l.ContentItemId,
                        ReactionType = l.ReactionType,
                        CreatedAt = l.CreatedAt,
                        Actor = ProjectMinimalActorStatic(l.Actor),
                        ContentItem = variant == IncludeVariant.FullDetails ? l.ContentItem : null
                    }).Cast<T>();

                IQueryable<T> ProjectFollow(IQueryable<Follow> q, IncludeVariant variant)
                    => q.Select(f => new Follow
                    {
                        FollowId = f.FollowId,
                        FollowerActorId = f.FollowerActorId,
                        FollowedActorId = f.FollowedActorId,
                        CreatedAt = f.CreatedAt,
                        FollowerActor = ProjectMinimalActorStatic(f.FollowerActor),
                        FollowedActor = ProjectMinimalActorStatic(f.FollowedActor)
                    }).Cast<T>();

                IQueryable<T> ProjectBotActivity(IQueryable<BotActivity> q, IncludeVariant variant)
                    => q.Select(b => new BotActivity
                    {
                        BotActivityId = b.BotActivityId,
                        BotId = b.BotId,
                        AdditionalId = b.AdditionalId,
                        AdditionalIdType = b.AdditionalIdType,
                        Message = b.Message,
                        IsRead = b.IsRead,
                        CreatedAt = b.CreatedAt,
                        Bot = ProjectMinimalBotStatic(b.Bot)
                    }).Cast<T>();

                IQueryable<T> ProjectNotification(IQueryable<Notification> q, IncludeVariant variant)
                    => q.Select(n => new Notification
                    {
                        NotificationId = n.NotificationId,
                        ReceiverUserId = n.ReceiverUserId,
                        SenderActorId = n.SenderActorId,
                        AdditionalId = n.AdditionalId,
                        AdditionalIdType = n.AdditionalIdType,
                        Message = n.Message,
                        BotActivityId = n.BotActivityId,
                        IsRead = n.IsRead,
                        CreatedAt = n.CreatedAt,
                        ReceiverUser = ProjectMinimalUserStatic(n.ReceiverUser),
                        SenderActor = ProjectMinimalActorStatic(n.SenderActor),
                        BotActivity = variant == IncludeVariant.FullDetails ? n.BotActivity : null
                    }).Cast<T>();

                IQueryable<T> ProjectTribe(IQueryable<Tribe> q, IncludeVariant variant)
                    => q.Select(tr => new Tribe
                    {
                        TribeId = tr.TribeId,
                        TribePoint = tr.TribePoint,
                        ImageUrl = tr.ImageUrl,
                        TribeName = tr.TribeName,
                        Mission = tr.Mission,
                        PersonalityModifier = tr.PersonalityModifier,
                        InstructionModifier = tr.InstructionModifier,
                        MemberCount = tr.MemberCount,
                        CreatedAt = tr.CreatedAt,
                        OutgoingRivalries = variant == IncludeVariant.FullDetails && tr.OutgoingRivalries != null
                            ? tr.OutgoingRivalries.Select(or => new TribeRivalry
                                {
                                    TribeRivalryId = or.TribeRivalryId,
                                    InitiatingTribeId = or.InitiatingTribeId,
                                    TargetTribeId = or.TargetTribeId,
                                    CreatedAt = or.CreatedAt,
                                    InitiatingTribe = null,
                                    TargetTribe = or.TargetTribe
                                }).ToList()
                            : null,
                        IncomingRivalries = variant == IncludeVariant.FullDetails && tr.IncomingRivalries != null
                            ? tr.IncomingRivalries.Select(ir => new TribeRivalry
                                {
                                    TribeRivalryId = ir.TribeRivalryId,
                                    InitiatingTribeId = ir.InitiatingTribeId,
                                    TargetTribeId = ir.TargetTribeId,
                                    CreatedAt = ir.CreatedAt,
                                    InitiatingTribe = ir.InitiatingTribe,
                                    TargetTribe = null
                                }).ToList()
                            : null,
                        TribeMemberships = tr.TribeMemberships != null
                            ? tr.TribeMemberships.Select(tm => new TribeMembership
                                {
                                    TribeMemberId = tm.TribeMemberId,
                                    ActorId = tm.ActorId,
                                    TribeId = tm.TribeId,
                                    CreatedAt = tm.CreatedAt,
                                    Actor = ProjectMinimalActorStatic(tm.Actor),
                                    Tribe = null
                                }).ToList()
                            : null
                    }).Cast<T>();

                IQueryable<T> projected = typeof(T) switch
                {
                    Type t when t == typeof(Post) => ProjectPost((IQueryable<Post>)query, includeVariant),
                    Type t when t == typeof(Entry) => ProjectEntry((IQueryable<Entry>)query, includeVariant),
                    Type t when t == typeof(Like) => ProjectLike((IQueryable<Like>)query, includeVariant),
                    Type t when t == typeof(Follow) => ProjectFollow((IQueryable<Follow>)query, includeVariant),
                    Type t when t == typeof(BotActivity) => ProjectBotActivity((IQueryable<BotActivity>)query, includeVariant),
                    Type t when t == typeof(Notification) => ProjectNotification((IQueryable<Notification>)query, includeVariant),
                    Type t when t == typeof(Tribe) => ProjectTribe((IQueryable<Tribe>)query, includeVariant),
                    _ => query
                };

                var result = await projected.ToListAsync();

                _logger.LogInformation("{Type} reload completed. Retrieved {Count} items.", typeof(T).Name, result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query could not be executed");
                throw;
            }
        }

        public static User? ProjectMinimalActorStatic(Actor? a)
            => a == null ? null : new User { ActorId = a.ActorId, ProfileName = a.ProfileName, ImageUrl = a.ImageUrl };
        public static Bot? ProjectMinimalBotStatic(Bot? b)
            => b == null ? null : new Bot { ActorId = b.ActorId, ProfileName = b.ProfileName, ImageUrl = b.ImageUrl };
        public static User? ProjectMinimalUserStatic(User? u)
            => u == null ? null : new User { ActorId = u.ActorId, ProfileName = u.ProfileName, ImageUrl = u.ImageUrl };
    }
}
