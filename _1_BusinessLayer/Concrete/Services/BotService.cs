using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.BotManagers;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class BotService : AbstractBotService
    {
        public BotService(AbstractBotRepository botRepository, BotDeployManager botManager, AbstractUserRepository userRepository, 
            AbstractPostRepository postRepository, AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository,
            AbstractActivityRepository activityRepository, AbstractFollowRepository followRepository) 
            : base(botRepository, botManager, userRepository, postRepository, entryRepository, likeRepository, activityRepository, followRepository)
        {
        }

        public override async Task<IdentityResult> CreateBot(int userId, CreateBotDto createBotDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
            if (user != null)
            {
                if (bots.Count() <= 4)
                {
                    var bot = createBotDto.CreateBotDto_To_Bot(userId);
                    await _botRepository.InsertAsync(bot);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new ForbiddenError("Bot limit reached"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DeleteBot(int userId, int botId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
            if (user != null)
            {
                foreach (var bot in bots)
                {
                    if (bot.BotId == botId)
                    {
                        await _botRepository.DeleteAsync(bot);
                        return IdentityResult.Failed();
                    }
                }
                return IdentityResult.Failed(new NotFoundError("This user does not have any type of that bot "));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DeployBot(int userId, int botId)
        {
            var user = await (_userRepository.GetByIdAsync(userId));
            List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
            if (user != null)
            {
                foreach (var bot in bots)
                {
                    if(bot.BotId == botId)
                    {
                        await _botDeployManager.BotDoActionAsync(bot);
                        bot.DailyOperationCheck = false;
                        await _botRepository.UpdateAsync(bot);
                        return IdentityResult.Success;
                    }
                }
                return IdentityResult.Failed(new NotFoundError("This user does not have any type of that bot "));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));

        }

        public override async Task<IdentityResult> EditBot(int userId, EditBotDto editBotDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var bot = await _botRepository.GetByIdAsync(editBotDto.BotId);
            if (user != null)
            {
                if (bot != null)
                {
                    bot = editBotDto.Update___EditBotDto_To_Bot(bot);
                    await _botRepository.UpdateAsync(bot);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new NotFoundError("This user does not have any type of that bot "));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<ObjectIdentityResult<BotProfileDto>> GetBotProfileAsync(int botId)
        {
            var bot = await _botRepository.GetByIdAsync(botId);
            var user = await _userRepository.GetByIdAsync(bot.UserId);
            var entryCount = await _entryRepository.GetEntryCountByBotIdAsync(botId);
            var postCount = await _postRepository.GetPostCountByBotIdAsync(botId);
            List<Entry> entries = await _entryRepository.GetAllByBotIdWithIntervalsAsync(botId,0,10);
            List<Post> posts = await _postRepository.GetAllByBotIdAsync(botId, 0, 10);
            List<Like> likes = await _likeRepository.GetAllByBotIdAsync(botId);
            List<Follow> followed = await _followRepository.GetAllByBotIdAsFollowerWithInfoAsync(botId);
            List<Follow> followers = await _followRepository.GetAllByBotIdAsFollowedWithInfoAsync(botId);
            foreach (var entry in entries)
            {
                entry.Bot = await _botRepository.GetByIdAsync((int)entry.BotId);
                entry.Likes = await _likeRepository.GetAllByEntryIdAsync((int)entry.EntryId);
                foreach (var entryLike in entry.Likes)
                {
                    entryLike.User = await _userRepository.GetByIdAsync((int)entryLike.UserId);
                    entryLike.Bot = await _botRepository.GetByIdAsync((int)entryLike.BotId);
                }
            }

            foreach (var post in posts)
            {
                post.Bot = await _botRepository.GetByIdAsync((int)post.BotId);
                post.Likes = await _likeRepository.GetAllByBotIdAsync((int)post.BotId);
                foreach (var postLike in post.Likes)
                {
                    postLike.Bot = await _botRepository.GetByIdAsync((int)postLike.BotId);
                    postLike.User = await _userRepository.GetByIdAsync((int)postLike.UserId);
                }
            }
            bot.User = user;
            bot.Entries = entries;
            bot.Likes = likes;
            bot.Posts = posts;
            bot.Followers = followers;
            bot.Followed = followed;
            var botProfileDto = bot.Bot_To_BotProfileDto();
            botProfileDto.EntryCount = entries.Count;
            botProfileDto.PostCount = posts.Count;
            return ObjectIdentityResult<BotProfileDto>.Succeded(botProfileDto);

        }

    }

}
