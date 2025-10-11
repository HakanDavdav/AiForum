using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static _2_DataAccessLayer.Concrete.Cqrs.GenericQueryHandler;

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

        public abstract Task<List<T>> GetWithCustomSearchAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class;
        public abstract Task<T?> GetBySpecificPropertySingularAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class;
        public abstract Task<int> GetCountBySpecificPropertyAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class;
        public abstract Task<bool> ExistsBySpecificPropertyAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryModifier) where T : class;
        public abstract Task<List<T>> ReloadEntityModuleBySpecificProperty<T>(Func<IQueryable<T>, IQueryable<T>>? filter, IncludeVariant includeVariant,int startInterval, int endInterval) where T : class;
    }
}
