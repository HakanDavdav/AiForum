using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRepository : AbstractUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger<User> logger) : base(context, logger)
        {
        }
        public override async Task<int> GetUnreadNotificationCountAsync(int id)
        {
            return await _context.Notifications.CountAsync(notification => notification.OwnerUserId == id && !notification.IsRead);
        }

        public override async Task<int> GetUnreadBotActivitiesCountAsync(int id)
        {
            return await _context.Activities.CountAsync(activity => activity.OwnerBot.ParentUserId == id && !activity.IsRead);
        }

        public override async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public override async Task DeleteAsync(User t)
        {
            try
            {
                _context.Users.Remove(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync for ParentUserId {ParentUserId}", t.Id);
                throw;
            }
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByIdAsync with ParentUserId {ParentUserId}", id);
                throw;
            }
        }

        public override async Task ManuallyInsertAsync(User t)
        {
            try
            {
                await _context.Users.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ManuallyInsertAsync for ParentUserId {ParentUserId}", t.Id);
                throw;
            }
        }

        public override async Task UpdateAsync(User t)
        {
            try
            {
                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for ParentUserId {ParentUserId}", t.Id);
                throw;
            }
        }


        public override async Task<User> GetBySpecificPropertySingularAsync(Func<IQueryable<User>, IQueryable<User>> queryModifier)
        {
            IQueryable<User> query = _context.Users;
            if (queryModifier != null)
                query = queryModifier(query);
#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override async Task<List<User>> GetWithCustomSearchAsync(Func<IQueryable<User>, IQueryable<User>> queryModifier)
        {
            IQueryable<User> query = _context.Users;
            if (queryModifier != null)
                query = queryModifier(query);
            return await query.ToListAsync();
        }

        public override async Task<User> GetUserModuleAsync(int id)
        {
            var user = await _context.Users
                .Where(user => user.Id == id)
                .Select(user => new User
                {
                    Id = user.Id,
                    ProfileName = user.ProfileName,
                    City = user.City,
                    DateTime = user.DateTime,
                    EntryCount = user.EntryCount,
                    PostCount = user.PostCount,
                    ImageUrl = user.ImageUrl,
                    FollowerCount = user.FollowerCount,
                    FollowedCount = user.FollowedCount,
                    LikeCount = user.LikeCount,
                    EmailConfirmed = user.EmailConfirmed,
                    Bots = user.Bots.Select(bot => new Bot
                    {
                        Id = bot.Id,
                        ImageUrl = bot.ImageUrl,
                        BotGrade = bot.BotGrade,
                        BotProfileName = bot.BotProfileName,
                        Mode = bot.Mode
                    }).ToList()
                })
                .FirstOrDefaultAsync(); // Tek bir kullanıcı çekiyoruz
            return user;


        }

        public override async Task ManuallyInsertRangeAsync(List<User> users)
        {
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<Bot>> GetBotsRecursivelyAsync(int id)
        {
            var user = await _context.Users
                             .Where(u => u.Id == id)
                             .Include(u => u.Bots)
                             .FirstOrDefaultAsync();

            if (user == null)
                return new List<Bot>();

            var botList = new List<Bot>();

            foreach (var bot in user.Bots)
            {
                await CollectBotsTreeAsync(bot, botList);
            }

            return botList;

            async Task CollectBotsTreeAsync(Bot bot, List<Bot> collectedBots)
            {
                collectedBots.Add(bot);

                // Properly await loading child bots
                await _context.Entry(bot)
                              .Collection(b => b.ChildBots)
                              .LoadAsync();

                foreach (var childBot in bot.ChildBots)
                {
                    await CollectBotsTreeAsync(childBot, collectedBots);
                }
            }
        }
    }
}
