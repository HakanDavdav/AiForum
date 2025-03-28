﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Abstractions.Generic
{
    public interface IGenericRepository<T> 
    {
        public Task<List<T>> GetAllWithCustomSearch(Func<IQueryable<T>, IQueryable<T>> queryModifier);
        public Task InsertAsync(T t);
        public Task DeleteAsync(T t);
        public Task UpdateAsync(T t);
        public Task<T> GetByIdAsync(int id);
        public Task<bool> CheckEntity(int id);
    }
}
