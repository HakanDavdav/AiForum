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
            Func<IQueryable<T>, IQueryable<T>>? filter
            int startInterval,
            int endInterval ) where T : class
        {
            try
            {
                IQueryable<T> query = _repository.Export<T>(_logger);

                // --- Include'lar ---
                query = typeof(T) switch
                {
                    Type t when t == typeof(Post) =>
                        ((IQueryable<Post>)query)
                            .Include(p => p.Actor)
                            .Cast<T>(),

                    Type t when t == typeof(Entry) =>
                        ((IQueryable<Entry>)query)
                            .Include(e => e.Actor)
                            .Cast<T>(),

                    Type t when t == typeof(Like) =>
                        ((IQueryable<Like>)query)
                            .Include(l => l.Actor)
                            .Cast<T>(),

                    Type t when t == typeof(Follow) =>
                        ((IQueryable<Follow>)query)
                            .Include(f => f.FollowerActor)
                            .Include(f => f.FollowedActor)
                            .Cast<T>(),

                    Type t when t == typeof(BotActivity) =>
                        ((IQueryable<BotActivity>)query)
                            .Include(b => b.Bot)
                            .Cast<T>(),

                    Type t when t == typeof(Notification) =>
                        ((IQueryable<Notification>)query)
                            .Include(n => n.SenderActor)
                            .Include(n => n.BotActivity)
                            .Cast<T>(),

                    Type t when t == typeof(Tribe) =>
                        ((IQueryable<Tribe>)query)
                            .Include(t => t.OutgoingRivalries)
                            .ThenInclude(or => or.TargetTribe)
                            .Include(t => t.IncomingRivalries)
                            .ThenInclude(ir => ir.InitiatingTribe)
                            .Include(t => t.TribeMemberships)
                            .ThenInclude(tm => tm.Actor)
                            .Cast<T>(),

                    _ => query
                };

                // --- Dışarıdan filtre uygulanabiliyor ---
                if (filter is not null)
                    query = filter(query);

                // --- Pagination ---
                query = query.Skip(startInterval).Take(endInterval - startInterval);

                var result = await query.ToListAsync();

                _logger.LogInformation("{Type} reload completed. Retrieved {Count} items.", typeof(T).Name, result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query could not be executed");
                throw;
            }
        }
    }
}
