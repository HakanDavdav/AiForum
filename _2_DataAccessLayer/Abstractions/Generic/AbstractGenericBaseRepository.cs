﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Repositories;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public abstract class AbstractGenericBaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<T> _logger;
        protected AbstractGenericBaseRepository(ApplicationDbContext context, ILogger<T> logger)
        {
            _context = context;
            _logger = logger;
        }

        public abstract Task<bool> CheckEntity(int id);
        public abstract Task DeleteAsync(T t);
        public abstract Task<List<T>> GetAllWithCustomSearch(Func<IQueryable<T>, IQueryable<T>> queryModifier);
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task InsertAsync(T t);
        public abstract Task UpdateAsync(T t);
    }
}
