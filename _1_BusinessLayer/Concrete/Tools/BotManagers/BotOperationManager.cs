using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _1_BusinessLayer.Concrete.Tools.BotManagers
{
    public class BotOperationManager
    {
        public AbstractEntryRepository _entryRepository;
        public AbstractPostRepository _postRepository;
        public AbstractLikeRepository _likeRepository;
        public AbstractFollowRepository _followRepository;
        public AbstractNotificationRepository _notificationRepository;
        public BotOperationManager(AbstractFollowRepository followRepository, AbstractEntryRepository entryRepository,
            AbstractLikeRepository likeRepository, AbstractPostRepository postRepository, AbstractNotificationRepository notificationRepository)
        {
            _entryRepository = entryRepository;
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _followRepository = followRepository;
            _notificationRepository = notificationRepository;
        }
        public Task<Notification> DoDatabaseOperation(Bot bot,string response, string aiResponseType)
        {
            switch (aiResponseType)
            {
                case "creatingEntry":
                    return CreateAiEntryResponse(bot, data);
                case "creatingOpposingEntry":
                    return CreateOpposingEntryResponse(bot, data);
                case "creatingPost":
                    return CreateAiPostResponse(bot, data);
                case "creatingUserFollowing":
                    return CreateAiFollowResponse(bot, data);
                case "creatingBotFollowing":
                    return CreateAiFollowResponse(bot, data);
                case "likePost":
                    return CreateAiLikeResponse(bot, data);
                case "likeEntry":
                    return CreateAiEntryResponse(bot, data);
                default:

                    throw new ArgumentException("Invalid responseType");
            }
        }

        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {
            
        }
        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {

        }
        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {

        }
        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {

        }
        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {

        }
        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {

        }
        public Task<Notification> EntryResponseOperation(Bot bot,string response)
        {

        }
    }
}
