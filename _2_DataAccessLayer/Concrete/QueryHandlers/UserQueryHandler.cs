using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.AbstractClasses;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.QueryHandlers
{
    public class UserQueryHandler : AbstractUserQueryHandler
    {
        public UserQueryHandler(ILogger<Actor> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public override async Task<Actor> GetUserWithBotTreeAsync(int id)
        {
            var user = await  _commandHandler.Export<Actor>()
                             .Where(u => u.ActorId == id)
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


        public override async Task<Actor> GetUserModuleAsync(int id)
        {
            var user = await _commandHandler.Export<Actor>()
                .Where(user => user.ActorId == id)
                .Select(user => new Actor
                {
                    ActorId = user.Id,
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
                        BotMode = bot.BotMode
                    }).ToList(),
                    UserSettings = user.UserPreference,
                })
                .FirstOrDefaultAsync(); // Tek bir kullanıcı çekiyoruz
            return user;


        }
    }
}
