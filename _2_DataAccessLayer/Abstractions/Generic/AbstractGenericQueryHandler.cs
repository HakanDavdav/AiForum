using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericQueryHandler
    {
        protected readonly Repository _repository;
        protected readonly ILogger _logger;

        protected AbstractGenericQueryHandler(Repository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<List<T>> GetWithCustomSearchAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(GetWithCustomSearchAsync),
                ["Entity"] = typeof(T).Name
            }))
            {
                var query = _repository.Export<T>(_logger);

                if (queryModifier != null)
                    query = queryModifier(query);

                _logger.LogInformation("Executing query: {Query}", query.ToQueryString());

                var result = await query.ToListAsync();

                _logger.LogInformation("Query executed. Result count: {Count}", result.Count);

                return result;
            }
        }

        public virtual async Task<T?> GetBySpecificPropertySingularAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(GetBySpecificPropertySingularAsync),
                ["Entity"] = typeof(T).Name
            }))
            {
                var query = _repository.Export<T>(_logger);

                if (queryModifier != null)
                    query = queryModifier(query);

                _logger.LogInformation("Executing singular query: {Query}", query.ToQueryString());

                var result = await query.FirstOrDefaultAsync();

                _logger.LogInformation("Query executed. Result is null: {IsNull}", result == null);

                return result;
            }
        }

        public virtual async Task<int> GetCountBySpecificPropertyAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(GetCountBySpecificPropertyAsync),
                ["Entity"] = typeof(T).Name
            }))
            {
                var query = _repository.Export<T>(_logger);

                if (queryModifier != null)
                    query = queryModifier(query);

                _logger.LogInformation("Executing count query: {Query}", query.ToQueryString());

                var count = await query.CountAsync();

                _logger.LogInformation("Query executed. Count: {Count}", count);

                return count;
            }
        }

        public virtual async Task<bool> ExistsBySpecificPropertyAsync<T>(
            Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(ExistsBySpecificPropertyAsync),
                ["Entity"] = typeof(T).Name
            }))
            {
                var query = _repository.Export<T>(_logger);

                if (queryModifier != null)
                    query = queryModifier(query);

                _logger.LogInformation("Executing exists query: {Query}", query.ToQueryString());

                var exists = await query.AnyAsync();

                _logger.LogInformation("Query executed. Exists: {Exists}", exists);

                return exists;
            }
        }

        public virtual async Task<List<T>> ReloadEntityModuleBySpecificProperty<T>(
            int startInterval,
            int endInterval) where T : class
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(ReloadEntityModuleBySpecificProperty),
                ["Entity"] = typeof(T).Name
            }))
            {
                var query = _repository.Export<T>(_logger);
                _logger.LogInformation("Reload started. Interval: {Start}-{End}", startInterval, endInterval);

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

                    _logger.LogInformation("Post reload completed. Retrieved {Count} items.", result.Count);
                }
                else if (typeof(T) == typeof(Entry))
                {
                    result = await query.Skip(startInterval)
                        .Take(endInterval - s
