using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Dtos.UserDtos;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class BotMappers
    {
        public static MinimalBotDto Bot_To_MinimalBotDto(this Bot bot)
        {
            return new MinimalBotDto
            {
                BotId = bot.BotId,
                ImageUrl = bot.ImageUrl,
                ProfileName = bot.BotProfileName,
            };
        }

        public static BotProfileDto Bot_To_BotProfileDto(this Bot bot)
        {
            List<PostProfileDto> postProfileDtos = new List<PostProfileDto>();
            List<EntryProfileDto> entryProfileDtos = new List<EntryProfileDto>();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            List<FollowProfileDto> followerDtos = new List<FollowProfileDto>();
            List<FollowProfileDto> followedDtos = new List<FollowProfileDto>();
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
            return new BotProfileDto
            {
                Date = bot.DateTime,
                ProfileName = bot.BotProfileName,
                ImageUrl = bot.ImageUrl,
                BotId = bot.BotId,
                Entries = entryProfileDtos,
                Posts = postProfileDtos,
                Likes = minimalLikeDtos,
                Followers = followedDtos,
                Followed = followedDtos,
            };
        }

        public static Bot CreateBotDto_To_Bot(this CreateBotDto createBotDto, int userId)
        {
            return new Bot
            {
                UserId = userId,
                BotPersonality = createBotDto.BotPersonality,
                DailyBotOperationCount = createBotDto.DailyBotOperationCount,
                ImageUrl = createBotDto.ImageUrl,
                Instructions = createBotDto.Instructions,
                Mode = createBotDto.Mode,
                BotProfileName = createBotDto.BotProfileName,
            };
        }

        public static Bot Update___EditBotDto_To_Bot(this EditBotDto editBotDto,Bot bot)
        {
            bot.BotPersonality = editBotDto.BotPersonality;
            bot.BotProfileName = editBotDto.BotProfileName;
            bot.ImageUrl = editBotDto.ImageUrl;
            bot.Instructions = editBotDto.Instructions;
            bot.Mode = editBotDto.Mode;
            return bot;
        }


    }
}
