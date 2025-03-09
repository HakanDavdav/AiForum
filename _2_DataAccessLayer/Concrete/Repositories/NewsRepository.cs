using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class NewsRepository : AbstractNewsRepository
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(News t)
        {
            _context.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<News>> GetAllAsync()
        {
            IQueryable<News> news = _context.News;
            return await news.ToListAsync();
        }

        public override async Task<List<News>> GetAllWithInfoAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<News> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            News news = await _context.News.FirstOrDefaultAsync(news => news.NewsId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return news;
        }

        public override Task<News> GetByIdWithInfoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task InsertAsync(News t)
        {
            await _context.News.AddAsync(t); 
            await _context.SaveChangesAsync();
        }

        public override Task UpdateAsync(News t)
        {
            throw new NotImplementedException();
        }
    }
}
