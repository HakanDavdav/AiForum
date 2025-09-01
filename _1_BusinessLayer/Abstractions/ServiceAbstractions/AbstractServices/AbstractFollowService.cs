using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.ServiceAbstractions.IServices;
using _2_DataAccessLayer.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.ServiceAbstractions.AbstractServices
{
    public abstract class AbstractFollowService : IFollowService
    {
        protected readonly AbstractFollowRepository _followRepository;
        protected readonly AbstractUserRepository _userRepository;
        protected readonly AbstractBotRepository _botRepository;

        public AbstractFollowService(AbstractFollowRepository followRepository, AbstractUserRepository userRepository, AbstractBotRepository botRepository  )
        {
            _followRepository = followRepository;
            _userRepository = userRepository;
            _botRepository = botRepository;
        }

        public abstract Task<IdentityResult> DeleteFollow(int userId, int followId);
        public abstract Task<IdentityResult> FollowBot(int userId, int followedBotId);
        public abstract Task<IdentityResult> FollowUser(int userId, int followedUserId);
    }
}
