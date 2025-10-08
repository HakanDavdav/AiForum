using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Cqrs
{
    public class GenericCommandHandler : AbstractGenericCommandHandler
    {
        public GenericCommandHandler(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override Task ManuallyInsertAsync<T>(T entity) where T : class
        {
            _logger.LogInformation("Inserting entity of type {EntityType}", typeof(T).Name);
            _context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public override Task ManuallyInsertRangeAsync<T>(List<T> entities) where T : class
        {
            _logger.LogInformation("Inserting {Count} entities of type {EntityType}", entities.Count, typeof(T).Name);
            _context.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }

        public override Task DeleteAsync<T>(T entity) where T : class
        {
            _logger.LogInformation("Deleting entity of type {EntityType}", typeof(T).Name);
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public override Task DeleteRangeAsync<T>(List<T> entities) where T : class
        {
            _logger.LogInformation("Deleting {Count} entities of type {EntityType}", entities.Count, typeof(T).Name);
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public override async Task SaveChangesAsync()
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
                            _logger.LogInformation("Added entity {Entity}", entityName);
                            break;

                        case EntityState.Modified:
                            var changes = entry.Properties
                                .Where(p => p.IsModified)
                                .Select(p => $"{p.Metadata.Name}: '{p.OriginalValue}' -> '{p.CurrentValue}'")
                                .ToList();
                            _logger.LogInformation("Modified entity {Entity} with changes: {Changes}", entityName, string.Join(", ", changes));
                            break;

                        case EntityState.Deleted:
                            _logger.LogInformation("Deleted entity {Entity}", entityName);
                            break;
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("SaveChangesAsync completed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveChangesAsync cannot executed");
                throw;
            }
        }
    }
}
