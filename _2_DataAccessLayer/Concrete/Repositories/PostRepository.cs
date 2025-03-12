﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class PostRepository : AbstractPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }


        public override async Task DeleteAsync(Post t)
        {

            _context.Posts.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<IQueryable<Post>> GetAllAsync()
        {
            IQueryable<Post> posts = _context.Posts;
            return posts;
        }

        public override async Task<IQueryable<Post>> GetAllWithInfoAsync()
        {                                          
                                                   //Post Owner User
            IQueryable<Post> posts = _context.Posts.Include(post => post.User)
                                                   //Post Owner Bot
                                                   .Include(post => post.Bot)
                                                   //Entry Owner User
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.User)
                                                   //Entry Owner Bot
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.Bot)
                                                   //Entry Likes With Liked Users
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.Likes)
                                                   .ThenInclude(like => like.User)
                                                   //Entry Likes With Liked Bots
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.Likes)
                                                   .ThenInclude(like => like.Bot)
                                                   //Post User Likes And Bot Likes
                                                   .Include(post => post.Likes)
                                                   .ThenInclude(like => like.User)
                                                   ////Post User Likes And Bot Likes
                                                   .Include(post => post.Likes)
                                                   .ThenInclude(like => like.Bot);


            return posts;
        }


        public override async Task<Post> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Post post = await _context.Posts.FirstOrDefaultAsync(post => post.PostId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }

        public override async Task<Post> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.                                                   
                                                   //Post Owner User
            Post post = await _context.Posts.Include(post => post.User)
                                                   //Post Owner Bot
                                                   .Include(post => post.Bot)
                                                   //Entry Owner User
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.User)
                                                   //Entry Owner Bot
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.Bot)
                                                   //Entry Likes With Liked Users
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.Likes)
                                                   .ThenInclude(like => like.User)
                                                   //Entry Likes With Liked Bots
                                                   .Include(post => post.Entries)
                                                   .ThenInclude(entry => entry.Likes)
                                                   .ThenInclude(like => like.Bot)
                                                   //Post User Likes And Bot Likes
                                                   .Include(post => post.Likes)
                                                   .ThenInclude(like => like.User)
                                                   ////Post User Likes And Bot Likes
                                                   .Include(post => post.Likes)
                                                   .ThenInclude(like => like.Bot)
                                                   .FirstOrDefaultAsync(post => post.PostId == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }



        public override async Task<Post> GetByTitleAsync(string title)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Post post = await _context.Posts.FirstOrDefaultAsync(post => post.Title == title);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return post;
        }


        public override async Task InsertAsync(Post t)
        {
            await _context.Posts.AddAsync(t);
            await _context.SaveChangesAsync();
        }


        public override async Task UpdateAsync(Post t)
        {
            _context.Update(t);
            await _context.SaveChangesAsync();

        }
        
    }
}
