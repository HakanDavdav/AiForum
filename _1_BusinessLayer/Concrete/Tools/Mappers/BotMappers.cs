using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class BotMappers
    {
        public static Bot CreateBotDto_To_Bot(this CreateBotDto createBotDto,User user)
        {
            return new Bot
            {
                UserId = user.Id,
                BotProfileName = createBotDto.BotProfileName,
                BotPersonality = createBotDto.BotPersonality,
                DailyBotActionCount = createBotDto.DailyBotOperationCount,
                Instructions = createBotDto.Instructions,
                Mode = createBotDto.Mode,
                ImageUrl = createBotDto.ImageUrl,
            };
        }

        public static BotProfileDto Bot_To_BotProfileDto(this Bot bot)
        {
            return new BotProfileDto
            {
                BotId = bot.BotId,
                BotProfileName = bot.BotProfileName,
                Date = bot.DateTime,
                Entries = bot.Entries,
                Posts = bot.Posts,
                Followers = bot.Followers,
                Followings = bot.Followings,
                ImageUrl = bot.ImageUrl,
                Likes = bot.Likes,
                UserId = bot.UserId,
            };
        }

        public static BotSearchBarDto Bot_To_BotSearchBarDto(this Bot bot)
        {
            return new BotSearchBarDto
            {
                BotId = bot.BotId,
                BotProfileName = bot.BotProfileName,
                ImageUrl = bot.ImageUrl,
                UserId = bot.UserId
            };
        }

        public static Bot Update___EditBotDto_To_Bot(this EditBotDto customizeBotDto, Bot bot)
        {
            bot.BotProfileName = customizeBotDto.BotProfileName;
            bot.BotPersonality = customizeBotDto.BotPersonality;
            bot.DailyBotActionCount = customizeBotDto.DailyBotOperationCount;
            bot.Instructions = customizeBotDto.Instructions;
            bot.Mode = customizeBotDto.Mode;
            bot.ImageUrl = customizeBotDto.ImageUrl;
            return bot;
        }



    }
}
