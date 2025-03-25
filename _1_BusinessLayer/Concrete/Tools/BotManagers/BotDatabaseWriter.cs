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
    public class BotDatabaseWriter
    {
        public AbstractEntryRepository _entryRepository;
        public AbstractPostRepository _postRepository;
        public AbstractLikeRepository _likeRepository;
        public AbstractFollowRepository _followRepository;
        public AbstractNotificationRepository _notificationRepository;
        public BotDatabaseWriter(AbstractFollowRepository followRepository, AbstractEntryRepository entryRepository,
            AbstractLikeRepository likeRepository, AbstractPostRepository postRepository, AbstractNotificationRepository notificationRepository)
        {
            _entryRepository = entryRepository;
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _followRepository = followRepository;
            _notificationRepository = notificationRepository;
        }
        public Task<Notification> WriteOnDatabase(Bot bot,int requiredId,string filteredAiResponse, string parseResponseType)
        {
            switch (parseResponseType)
            {
                case "creatingEntry":
                    return WriteEntry(bot, filteredAiResponse, requiredId);
                case "creatingOpposingEntry":
                    return WriteOpposingEntry(bot, filteredAiResponse, requiredId);
                case "creatingPost":
                    return WriteOpposingEntry(bot, filteredAiResponse, requiredId);
                case "creatingUserFollowing":
                    return WriteUserFollow(bot, filteredAiResponse, requiredId);
                case "creatingBotFollowing":
                    return WriteBotFollow(bot, filteredAiResponse, requiredId);
                case "likePost":
                    return WriteLikePost(bot, filteredAiResponse, requiredId);
                case "likeEntry":
                    return WriteLikeEntry(bot, filteredAiResponse, requiredId);
                default:

                    throw new ArgumentException("Invalid responseType");
            }
        }

        public Task<Notification> WriteEntry(Bot bot,string filteredAiResponse,int requiredID)
        {
            throw new NotImplementedException();
        }
        public Task<Notification> WriteOpposingEntry(Bot bot, string filteredAiResponse, int requiredID)
        {
            throw new NotImplementedException();
        }
        public Task<Notification> WritePost(Bot bot, string filteredAiResponse, int requiredID)
        {
            throw new NotImplementedException();
        }
        public Task<Notification> WriteBotFollow(Bot bot, string filteredAiResponse, int requiredID)
        {
            throw new NotImplementedException();
        }
        public Task<Notification> WriteUserFollow(Bot bot, string filteredAiResponse, int requiredID)
        {
            throw new NotImplementedException();
        }
        public Task<Notification> WriteLikePost(Bot bot, string filteredAiResponse, int requiredID)
        {
            throw new NotImplementedException();
        }
        public Task<Notification> WriteLikeEntry(Bot bot, string filteredAiResponse, int requiredID)
        {
            throw new NotImplementedException();

        }
    }
}
