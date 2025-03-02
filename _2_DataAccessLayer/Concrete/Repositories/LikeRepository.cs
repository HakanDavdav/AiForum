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
    public class LikeRepository : AbstractLikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
        }



        public override async Task DeleteAsync(Like t)
        {
            _context.likes.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<Like>> GetAllAsync()
        {
            IQueryable<Like> likes = _context.likes;
            return await likes.ToListAsync();
        }

        public override async Task<List<Like>> GetAllWithInfoAsync()
        {
            IQueryable<Like> likes = _context.likes.Include(like => like.User)
                                       .Include(like => like.Bot)
                                       .Include(like => like.Post)
                                       .Include(like => like.Entry);
            return await likes.ToListAsync();
        }

        public override async Task<Like> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Like like = await _context.likes.FirstOrDefaultAsync(like => like.LikeId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return like;
        }

        public override async Task<Like> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Like like = await _context.likes.Include(like => like.User)
                                            .Include(like => like.Bot)
                                            .Include(like => like.Post)
                                            .Include(like => like.Entry)
                                            .FirstOrDefaultAsync(like => like.LikeId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return like;
        }

        public override async Task InsertAsync(Like t)
        {
            await _context.likes.AddAsync(t);
            await _context.SaveChangesAsync();
        }


        public override async Task UpdateAsync(Like t)
        {
            _context.likes.Attach(t);
            await _context.SaveChangesAsync();
        }

    }
}
