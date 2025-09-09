using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.FollowDto;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.BodyBuilders;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Managers.BotManagers;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _1_BusinessLayer.Concrete.Services
{
    public class BotService : AbstractBotService
    {
        public BotService(AbstractBotRepository botRepository, BotDeployManager botManager, AbstractUserRepository userRepository,
            AbstractPostRepository postRepository, AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository,
            AbstractActivityRepository activityRepository, AbstractFollowRepository followRepository, NotificationActivityBodyBuilder notificationActivityBodyBuilder)
            : base(botRepository, botManager, userRepository, postRepository, entryRepository, likeRepository, activityRepository, followRepository, notificationActivityBodyBuilder)
        {
        }

        public override async Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(bot => bot.Id == userId).Include(bot => bot.Bots));
            if (user == null) return IdentityResult.Failed(new NotFoundError("User not found"));
            if (user.Bots.Count > 4) return IdentityResult.Failed(new ForbiddenError("Personal bot limit reached"));
            var bot = createBotDto.CreateBotDto_To_Bot(userId);
            user.Bots.Add(bot);
            await _userRepository.SaveChangesAsync();
            return IdentityResult.Success;

        }

        public override async Task<IdentityResult> DeleteBot(int userId, int botId)
        {
            var user = await _userRepository.GetBySpecificPropertySingularAsync(query => query.Where(bot => bot.Id == userId).Include(bot => bot.Bots));
            if (user == null) return IdentityResult.Failed(new NotFoundError("User not found"));
            var bot = user.Bots.FirstOrDefault(bot => bot.Id == botId);
            if (bot == null) return IdentityResult.Failed(new NotFoundError("This user does not have any type of that bot "));
            user.Bots.Remove(bot);
            await _userRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> DeployBot(int userId, int botId)
        {
            throw new NotImplementedException();
        }


        public override async Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId, ClaimsPrincipal claims)
        {
            var startInterval = 0;
            var endInterval = claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10;
            var bot = await _botRepository.GetBotModuleAsync(botId);
            if (bot == null) return ObjectIdentityResult<BotProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("ParentBot not found") });
            bot.Entries = await _entryRepository.GetEntryModulesForBotAsync(botId, startInterval, endInterval);
            bot.Posts = await _postRepository.GetPostModulesForBot(botId, startInterval, endInterval);
            bot.Followers = await _followRepository.GetFollowModulesForBotAsFollowedAsync(botId, startInterval, endInterval);
            bot.Followed = await _followRepository.GetFollowModulesForBotAsFollowerAsync(botId, startInterval, endInterval);
            bot.Activities = await _activityRepository.GetBotActivityModulesForBotAsync(botId, startInterval, endInterval);
            var botProfileDto = bot.Bot_To_BotProfileDto(_notificationActivityBodyBuilder);
            return ObjectIdentityResult<BotProfileDto>.Succeded(botProfileDto);

        }

        public override async Task<ObjectIdentityResult<List<EntryProfileDto>>> LoadProfileEntries(int botId, ClaimsPrincipal claims, int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var entries = await _entryRepository.GetEntryModulesForBotAsync(botId, startInterval, endInterval);
            var entryProfileDtos = new List<EntryProfileDto>();
            foreach (var entry in entries)
            {
                entryProfileDtos.Add(entry.Entry_To_EntryProfileDto());
            }
            return ObjectIdentityResult<List<EntryProfileDto>>.Succeded(entryProfileDtos);

        }

        public override async Task<ObjectIdentityResult<List<PostProfileDto>>> LoadProfilePosts(int botId, ClaimsPrincipal claims, int page)
        {
            var startInterval = (page - 1) * (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var endInterval = startInterval + (claims.FindFirst("EntryPerPage") != null ? int.Parse(claims.FindFirst("EntryPerPage").Value) : 10);
            var posts = await _postRepository.GetPostModulesForBot(botId, startInterval, endInterval);
            var postProfileDtos = new List<PostProfileDto>();
            foreach (var post in posts)
            {
                postProfileDtos.Add(post.Post_To_PostProfileDto());
            }
            return ObjectIdentityResult<List<PostProfileDto>>.Succeded(postProfileDtos);

        }


        public override async Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowed(int botId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var follows = await _followRepository.GetFollowModulesForBotAsFollowerAsync(botId, startInterval, endInterval);
            List<FollowProfileDto> followProfileDtos = new List<FollowProfileDto>();
            foreach (var follow in follows)
            {
                followProfileDtos.Add(follow.Follow_To_FollowProfileDto());
            }
            return ObjectIdentityResult<List<FollowProfileDto>>.Succeded(followProfileDtos);

        }

        public override async Task<ObjectIdentityResult<List<FollowProfileDto>>> LoadFollowers(int botId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var follows = await _followRepository.GetFollowModulesForBotAsFollowedAsync(botId, startInterval, endInterval);
            List<FollowProfileDto> followProfileDtos = new List<FollowProfileDto>();
            foreach (var follow in follows)
            {
                followProfileDtos.Add(follow.Follow_To_FollowProfileDto());
            }
            return ObjectIdentityResult<List<FollowProfileDto>>.Succeded(followProfileDtos);
        }

        public override async Task<ObjectIdentityResult<List<BotActivityDto>>> LoadBotActivities(int botId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var botActivities = await _activityRepository.GetBotActivityModulesForBotAsync(botId, startInterval, endInterval);
            List<BotActivityDto> botActivityDtos = new List<BotActivityDto>();
            foreach (var activity in botActivities)
            {
                var (title, body) = _notificationActivityBodyBuilder.BuildAppBotActivityContent(activity);
                botActivityDtos.Add(activity.BotActivity_To_BotActivityDto(title, body));
                activity.IsRead = true;
            }
            return ObjectIdentityResult<List<BotActivityDto>>.Succeded(botActivityDtos);
        }

        public override async Task<ObjectIdentityResult<List<MinimalLikeDto>>> LoadBotLikes(int botId, int page)
        {
            var startInterval = (page - 1) * 10;
            var endInterval = startInterval + 10;
            var botLikes = await _likeRepository.GetLikeModulesForBot(botId, startInterval, endInterval);
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in botLikes)
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return ObjectIdentityResult<List<MinimalLikeDto>>.Succeded(minimalLikeDtos);
        }

        public override async Task<ObjectIdentityResult<MinimalBotDto>> GetBotWithChildBotsTree(int botId)
        {
            var bot = await _botRepository.GetBotWithChildBotTree(botId);
            if (bot == null) return ObjectIdentityResult<MinimalBotDto>.Failed(null, new IdentityError[] { new NotFoundError("Bot not found") });
            var minimalBotDto = bot.BotWithBotTree_To_MinimalVersion();
            return ObjectIdentityResult<MinimalBotDto>.Succeded(minimalBotDto);
        }
    }

}
