using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace _1_BusinessLayer.Concrete.Tools.Extensions.Mappers
{
    public static class BotMappers
    {
        public static MinimalBotDto Bot_To_MinimalBotDto(this Bot? bot)
        {
            if (bot == null) return null;
            return new MinimalBotDto
            {
                BotId = bot.Id,
                ImageUrl = bot.ImageUrl,
                ProfileName = bot.BotProfileName,
            };
        }

        public static BotSettingsDto Bot_To_BotSettingsDto(this Bot? bot)
        {
            return new BotSettingsDto
            {
                BotPersonality = bot.BotPersonality,
                DailyBotOperationCount = bot.DailyBotOperationCount,
                ImageUrl = bot.ImageUrl,
                Instructions = bot.Instructions,
                Mode = bot.BotMode,
                BotProfileName = bot.BotProfileName,
            };
        }

        public static BotProfileDto Bot_To_BotProfileDto(this Bot bot, NotificationActivityBodyBuilder notificationActivityBodyBuilder)
        {
            List<PostProfileDto> postProfileDtos = new List<PostProfileDto>();
            List<EntryProfileDto> entryProfileDtos = new List<EntryProfileDto>();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            List<FollowProfileDto> followerDtos = new List<FollowProfileDto>();
            List<FollowProfileDto> followedDtos = new List<FollowProfileDto>();
            List<BotActivityDto> botActivityDtos = new List<BotActivityDto>();
            List<MinimalBotDto> childBotDtos = new List<MinimalBotDto>();
            foreach (var post in bot.Posts ?? new List<Post>())
            {
                postProfileDtos.Add(post.Post_To_PostProfileDto());
            }
            foreach (var entry in bot.Entries ?? new List<Entry>())
            {
                entryProfileDtos.Add(entry.Entry_To_EntryProfileDto());
            }
            foreach (var like in bot.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            foreach (var follower in bot.Followers ?? new List<Follow>())
            {
                followerDtos.Add(follower.Follow_To_FollowProfileDto());
            }
            foreach (var followed in bot.Followed ?? new List<Follow>())
            {
                followedDtos.Add(followed.Follow_To_FollowProfileDto());
            }
            foreach (var activity in bot.Activities ?? new List<BotActivity>())
            {
                var (title, body) = notificationActivityBodyBuilder.BuildAppBotActivityContent(activity);
                botActivityDtos.Add(activity.BotActivity_To_BotActivityDto(body, title));
            }
            foreach (var childBot in bot.ChildBots ?? new List<Bot>())
            {
                childBotDtos.Add(childBot.Bot_To_MinimalBotDto());
            }
            return new BotProfileDto
            {
                Date = bot.DateTime,
                EntryCount = bot.EntryCount,
                FollowerCount = bot.FollowerCount,
                FollowedCount = bot.FollowedCount,
                PostCount = bot.PostCount,
                LikeCount = bot.LikeCount,
                ProfileName = bot.BotProfileName,
                BotGrade = bot.BotGrade,
                ImageUrl = bot.ImageUrl,
                BotId = bot.Id,
                Entries = entryProfileDtos,
                Posts = postProfileDtos,
                Likes = minimalLikeDtos,
                Followers = followedDtos,
                Followed = followedDtos,
                BotActivities = botActivityDtos,
                ChildBots = childBotDtos
            };
        }

        public static MinimalBotDto BotWithBotTree_To_MinimalVersion(this Bot bot)
        {
            return ConvertBotTree(bot);

            static MinimalBotDto ConvertBotTree(Bot bot)
            {
                return new MinimalBotDto
                {
                    BotId = bot.Id,
                    ProfileName = bot.BotProfileName,
                    ImageUrl = bot.ImageUrl,
                    // Recursively convert child bots
                    ChildBots = bot.ChildBots?.Select(childBot => ConvertBotTree(childBot)).ToList()
                };
            }
        }

        public static Bot CreateBotDto_To_Bot(this CreateBotDto createBotDto, int userId)
        {
            return new Bot
            {
                ParentUserId = userId,
                BotPersonality = createBotDto.BotPersonality,
                DailyBotOperationCount = createBotDto.DailyBotOperationCount,
                ImageUrl = createBotDto.ImageUrl,
                Instructions = createBotDto.Instructions,
                BotMode = createBotDto.Mode,
                BotProfileName = createBotDto.BotProfileName,
            };
        }

        public static Bot Update___EditBotDto_To_Bot(this EditBotDto editBotDto, Bot bot)
        {
            bot.BotPersonality = editBotDto.BotPersonality;
            bot.BotProfileName = editBotDto.BotProfileName;
            bot.ImageUrl = editBotDto.ImageUrl;
            bot.Instructions = editBotDto.Instructions;
            bot.BotMode = editBotDto.Mode;
            return bot;
        }


    }
}
