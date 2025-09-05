using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Managers.BotManagers;
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
                    await _botRepository.ManuallyInsertAsync(bot);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new ForbiddenError("OwnerBot limit reached"));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> DeleteBot(int userId, int botId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
            if (user != null)
            {
                foreach (var bot in bots)
                {
                    if (bot.Id == botId)
                    {
                        await _botRepository.DeleteAsync(bot);
                        return IdentityResult.Failed();
                    }
                }
                return IdentityResult.Failed(new NotFoundError("This user does not have any BotActivityType of that bot "));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<IdentityResult> DeployBot(int userId, int botId)
        {
            var user = await (_userRepository.GetByIdAsync(userId));
            List<Bot> bots = await _botRepository.GetAllByUserIdAsync(userId);
            if (user != null)
            {
                foreach (var bot in bots)
                {
                    if(bot.Id == botId)
                    {
                        await _botDeployManager.BotDoActionAsync(bot);
                        bot.DailyOperationCheck = false;
                        await _botRepository.UpdateAsync(bot);
                        return IdentityResult.Success;
                    }
                }
                return IdentityResult.Failed(new NotFoundError("This user does not have any BotActivityType of that bot "));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));

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
                return IdentityResult.Failed(new NotFoundError("This user does not have any BotActivityType of that bot "));
            }
            return IdentityResult.Failed(new NotFoundError("OwnerUser not found"));
        }

        public override async Task<ObjectIdentityResult<BotProfileDto>> GetBotProfile(int botId, int entrPerPagePreference = 10)
        {
           
            var bot = listBot.FirstOrDefault();
            if (bot != null) {

                var entryCount = await _botRepository.GetEntryCountOfBotAsync(botId);
                var postCount = await _botRepository.GetPostCountOfBotAsync(botId);

                var botProfileDto = bot.Bot_To_BotProfileDto();
                botProfileDto.EntryCount = entryCount;
                botProfileDto.PostCount = postCount;
                return ObjectIdentityResult<BotProfileDto>.Succeded(botProfileDto);
            }
            return ObjectIdentityResult<BotProfileDto>.Failed(null, new IdentityError[] { new NotFoundError("OwnerBot not found") });


        }

        public override async Task<ObjectIdentityResult<List<Entry>>> LoadProfileEntries(int botId, int startInterval, int endInterval)
        {
            var bot = await _botRepository.GetByIdAsync(botId);
            if (bot != null)
            {
                var entries = await _entryRepository.GetEntryModulesForBotAsync(botId, startInterval, endInterval);
                return ObjectIdentityResult<List<Entry>>.Succeded(entries);
            }
            return ObjectIdentityResult<List<Entry>>.Failed(null, new IdentityError[] { new NotFoundError("OwnerBot not found") });
        }

        public override async Task<ObjectIdentityResult<List<Post>>> LoadProfilePosts(int botId, int startInterval, int endInterval)
        {
            var bot = await _botRepository.GetByIdAsync(botId);
            if (bot != null)
            {
                var posts = await _postRepository.GetPostModulesForBot(botId, startInterval, endInterval);
                return ObjectIdentityResult<List<Post>>.Succeded(posts);
            }
            return ObjectIdentityResult<List<Post>>.Failed(null, new IdentityError[] { new NotFoundError("OwnerBot not found") });
        }
    }

}
