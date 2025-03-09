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
        public static Bot CreateBotDto_To_Bot(this CreateBotDto createBotDto)
        {
            return new Bot
            {
                BotProfileName = createBotDto.BotProfileName,
                BotPersonality = createBotDto.BotPersonality,
                DailyBotMessageCount = createBotDto.DailyBotMessageCount,
                Instructions = createBotDto.Instructions,
                Mode = createBotDto.Mode,
                ImageUrl = createBotDto.ImageUrl,
            };
        }

        public static Bot Update___CustomizeBotDto_To_Bot(this CustomizeBotDto customizeBotDto,Bot bot)
        {
                bot.BotProfileName = customizeBotDto.BotProfileName;
                bot.BotPersonality = customizeBotDto.BotPersonality;
                bot.DailyBotMessageCount = customizeBotDto.DailyBotMessageCount;
                bot.Instructions = customizeBotDto.Instructions;
                bot.Mode = customizeBotDto.Mode;
                bot.ImageUrl = customizeBotDto.ImageUrl;
                return bot;
        }


    }
}
