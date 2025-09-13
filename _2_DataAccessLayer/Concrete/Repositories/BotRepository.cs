using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class BotRepository : AbstractBotRepository
    {
        public BotRepository(ApplicationDbContext context, ILogger<Bot> logger) : base(context, logger)
        {
        }

        public override async Task<Bot> GetBotModuleAsync(int id)
        {
            var bot = await _context.Bots.Where(bot => bot.Id == id).Select(
                bot => new Bot
                {
                    Id = bot.Id,
                    BotGrade = bot.BotGrade,
                    BotProfileName = bot.BotProfileName,
                    ImageUrl = bot.ImageUrl,
                    DateTime = bot.DateTime,
                    FollowerCount = bot.FollowerCount,
                    FollowedCount = bot.FollowedCount,
                    EntryCount = bot.EntryCount,
                    PostCount = bot.PostCount,
                    LikeCount = bot.LikeCount,
                    Mode = bot.Mode,
                    ParentUser = bot.ParentUser,
                    ParentUserId = bot.ParentUserId,
                    ParentBot = bot.ParentBot,
                    ParentBotId = bot.ParentBotId,
                    ChildBots = bot.ChildBots
                }).FirstOrDefaultAsync();
            return bot;
        }


        public override async Task<Bot> GetBotWithChildBotTree(int id)
        {
            var parentBot = await _context.Bots
                             .Where(u => u.Id == id)
                             .Include(u => u.ChildBots)
                             .FirstOrDefaultAsync();

            if (parentBot == null)
                throw new InvalidOperationException($"Bot with id {id} not found.");


            foreach (var bot in parentBot.ChildBots)
            {
                await CollectBotsTreeAsync(bot);
            }

            return parentBot;

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
