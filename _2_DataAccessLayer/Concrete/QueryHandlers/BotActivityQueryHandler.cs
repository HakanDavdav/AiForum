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
    public class BotActivityQueryHandler : AbstractBotActivityQueryHandler
    {
        public BotActivityQueryHandler(ILogger<BotActivity> logger, AbstractGenericCommandHandler repository) : base(logger, repository)
        {
        }

        public override Task<List<BotActivity>> GetBotActivityModulesForBotAsync(int botId, int startInterval, int endInterval)
        {
            try
            {
                var BotActivities = _repository.Export<BotActivity>().Where(activity => activity.OwnerBotId == botId)
            .Skip(startInterval).Take(endInterval - startInterval).Select(
                activity => new BotActivity
                {
                    
                    ActivityId = activity.ActivityId,
                    OwnerBotId = activity.OwnerBotId,
                    BotActivityType = activity.BotActivityType,
                    IsRead = activity.IsRead,
                    AdditionalId = activity.AdditionalId,
                    DateTime = activity.DateTime,
                    OwnerBot = activity.OwnerBot,
                });
                return BotActivities.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }



        public override async Task<List<BotActivity>> GetBotActivityModulesForUserAsync(int id, int startInterval, int endInterval)
        {
            var user = await _repository.Export<User>()
                             .Where(u => u.Id == id)
                             .Include(u => u.Bots)
                             .FirstOrDefaultAsync();

            if (user == null)
                return new List<BotActivity>();

            var botList = new List<Bot>();

            foreach (var bot in user.Bots)
            {
                await CollectBotsTreeAsync(bot, botList);
            }

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

            var botIds = botList.Select(bot => bot.Id).ToList();
            var BotActivities = _context.Activities.
                Where(activity => activity.OwnerBotId != null && botIds.Contains(activity.OwnerBotId.Value)).Skip(startInterval).Take(endInterval - startInterval).Select(
                activity => new BotActivity
                {
                    ActivityId = activity.ActivityId,
                    OwnerBotId = activity.OwnerBotId,
                    BotActivityType = activity.BotActivityType,
                    IsRead = activity.IsRead,
                    AdditionalId = activity.AdditionalId,
                    DateTime = activity.DateTime,
                    OwnerBot = activity.OwnerBot,
                });
            return await BotActivities.ToListAsync();
        }
    }
}
