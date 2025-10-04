using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericCommandHandler
    {
        protected readonly ApplicationDbContext _context;

        protected AbstractGenericCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual Task ManuallyInsertAsync<T>(T entity, ILogger logger, [CallerMemberName] string methodName = "") where T : class
        {
            logger.LogInformation($"{this.GetType().Name}.{methodName}: Inserting entity of type {typeof(T).Name}");
            _context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task ManuallyInsertRangeAsync<T>(List<T> entities, ILogger logger, [CallerMemberName] string methodName = "") where T : class
        {
            logger.LogInformation($"{this.GetType().Name}.{methodName}: Inserting {entities.Count} entities of type {typeof(T).Name}");
            _context.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync<T>(T entity, ILogger logger, [CallerMemberName] string methodName = "") where T : class
        {
            logger.LogInformation($"{this.GetType().Name}.{methodName}: Deleting entity of type {typeof(T).Name}");
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteRangeAsync<T>(List<T> entities, ILogger logger, [CallerMemberName] string methodName = "") where T : class
        {
            logger.LogInformation($"{this.GetType().Name}.{methodName}: Deleting {entities.Count} entities of type {typeof(T).Name}");
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync(ILogger logger, [CallerMemberName] string methodName = "")
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
                            logger.LogInformation("{Handler}.{Method}: Added entity {Entity}",
                                this.GetType().Name, methodName, entityName);
                            break;

                        case EntityState.Modified:
                            var changes = entry.Properties
                                .Where(p => p.IsModified)
                                .Select(p => $"{p.Metadata.Name}: '{p.OriginalValue}' -> '{p.CurrentValue}'")
                                .ToList();

                            logger.LogInformation("{Handler}.{Method}: Modified entity {Entity} with changes: {Changes}",
                                this.GetType().Name, methodName, entityName, string.Join(", ", changes));
                            break;

                        case EntityState.Deleted:
                            logger.LogInformation("{Handler}.{Method}: Deleted entity {Entity}",
                                this.GetType().Name, methodName, entityName);
                            break;
                    }
                }

                await _context.SaveChangesAsync();

                logger.LogInformation("{Handler}.{Method}: SaveChangesAsync completed successfully",
                    this.GetType().Name, methodName);
            }
            catch (Exception ex)
            {
                logger.LogWarning("{Handler}.{Method}: SaveChangesAsync failed", this.GetType().Name, methodName);
                logger.LogError(ex, "{Handler}.{Method}: Exception occurred during SaveChangesAsync", this.GetType().Name, methodName);
                throw;
            }
        }


    }
}
