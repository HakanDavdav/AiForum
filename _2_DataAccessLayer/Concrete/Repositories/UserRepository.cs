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
                    }).ToList(),
                    UserPreference = user.UserPreference,         
                })
                .FirstOrDefaultAsync(); // Tek bir kullanıcı çekiyoruz
            return user;


        }

        public override async Task<User> GetUserWithBotTreeAsync(int id)
        {
            var user = await _context.Users
                             .Where(u => u.Id == id)
                             .Include(u => u.Bots)
                             .FirstOrDefaultAsync();

            if (user == null)
                throw new InvalidOperationException($"User with id {id} not found.");

            var botList = new List<Bot>();

            foreach (var bot in user.Bots)
            {
                await CollectBotsTreeAsync(bot);
            }

            return user;

            async Task CollectBotsTreeAsync(Bot bot)
            {
                // Properly await loading child bots
                await _context.Entry(bot)
                              .Collection(b => b.ChildBots)
                              .LoadAsync();

                foreach (var childBot in bot.ChildBots)
                {
                    await CollectBotsTreeAsync(childBot);
                }
            }
        }
    }
}
