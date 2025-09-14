using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericBaseQueryHandler<T> where T : class
    {

        protected readonly ILogger<T> _logger;
        protected readonly AbstractGenericBaseCommandHandler _repository;

        protected AbstractGenericBaseQueryHandler(ILogger<T> logger, AbstractGenericBaseCommandHandler repository)
        {
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<List<T>> GetWithCustomSearchAsync(Func<IQueryable<T>, IQueryable<T>> queryModifier)
        {
            var query = _repository.Export<T>();
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }


        public virtual async Task<T> GetBySpecificPropertySingularAsync(Func<IQueryable<T>, IQueryable<T>> queryModifier)
        {
            var query = _repository.Export<T>();
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }


    }
}
