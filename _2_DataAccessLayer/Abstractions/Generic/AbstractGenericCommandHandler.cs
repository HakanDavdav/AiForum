using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericCommandHandler
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger _logger;

        protected AbstractGenericCommandHandler(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual Task ManuallyInsertAsync<T>(T entity) where T : class
        {
            _logger.LogInformation("{Handler}.{Method}: Inserting entity of type {EntityType}",
                this.GetType().Name, nameof(ManuallyInsertAsync), typeof(T).Name);

            _context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task ManuallyInsertRangeAsync<T>(List<T> entities) where T : class
        {
            _logger.LogInformation("{Handler}.{Method}: Inserting {Count} entities of type {EntityType}",
                this.GetType().Name, nameof(ManuallyInsertRangeAsync), entities.Count, typeof(T).Name);

            _context.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync<T>(T entity) where T : class
        {
            _logger.LogInformation("{Handler}.{Method}: Deleting entity of type {EntityType}",
                this.GetType().Name, nameof(DeleteAsync), typeof(T).Name);

            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteRangeAsync<T>(List<T> entities) where T : class
        {
            _logger.LogInformation("{Handler}.{Method}: Deleting {Count} entities of type {EntityType}",
                this.GetType().Name, nameof(DeleteRangeAsync), entities.Count, typeof(T).Name);

            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync()
        {
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["Handler"] = this.GetType().Name,
                ["Method"] = nameof(SaveChangesAsync),
                ["TransactionId"] = Guid.NewGuid()
            }))
            {
                try
                {
                    var entries = _context.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added ||
                                    e.State == EntityState.Modified ||
                                    e.State == EntityState.Deleted)
                        .ToList();

                    foreach (var entry in entries)
                    {
                        var entityName = entry.Entity.GetType().Name;

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                _logger.LogInformation("{Handler}.{Method}: Added entity {Entity}",
                                    this.GetType().Name, nameof(SaveChangesAsync), entityName);
                                break;

                            case EntityState.Modified:
                                var changes = entry.Properties
                                    .Where(p => p.IsModified)
                                    .Select(p => $"{p.Metadata.Name}: '{p.OriginalValue}' -> '{p.CurrentValue}'")
                                    .ToList();

                                _logger.LogInformation("{Handler}.{Method}: Modified entity {Entity} with changes: {Changes}",
                                    this.GetType().Name, nameof(SaveChangesAsync), entityName, string.Join(", ", changes));
                                break;

                            case EntityState.Deleted:
                                _logger.LogInformation("{Handler}.{Method}: Deleted entity {Entity}",
                                    this.GetType().Name, nameof(SaveChangesAsync), entityName);
                                break;
                        }
                    }

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("{Handler}.{Method}: SaveChangesAsync completed successfully",
                        this.GetType().Name, nameof(SaveChangesAsync));
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("{Handler}.{Method}: SaveChangesAsync failed",
                        this.GetType().Name, nameof(SaveChangesAsync));
                    _logger.LogError(ex, "{Handler}.{Method}: Exception occurred during SaveChangesAsync",
                        this.GetType().Name, nameof(SaveChangesAsync));
                    throw;
                }
            }
        }
    }
}
