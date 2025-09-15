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
    public class BotQueryHandler : AbstractBotQueryHandler
    {
        public BotQueryHandler(ILogger<Bot> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public override async Task<Bot> GetBotModuleAsync(int id)
        {
            try
            {
                var bot = await _commandHandler.Export<Bot>().Where(bot => bot.Id == id).Select(
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
                BotMode = bot.BotMode,
                ParentUser = bot.ParentUser,
                ParentUserId = bot.ParentUserId,
                ParentBot = bot.ParentBot,
                ParentBotId = bot.ParentBotId,
                ChildBots = bot.ChildBots
            }).FirstOrDefaultAsync();
                return bot;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public override async Task<Bot> GetBotWithChildBotTreeAsync(int id)
        {
            var parentBot = await _commandHandler.Export<Bot>()  
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
