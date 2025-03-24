using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class FollowService : AbstractFollowService
    {
        public FollowService(AbstractFollowRepository followRepository, AbstractUserRepository userRepository) : base(followRepository, userRepository)
        {
        }

        public override async Task<IdentityResult> DeleteFollow(int userId, int followId)
        {
            var user = _userRepository.GetByIdAsync(userId);
            List<Follow> follows = new List<Follow>();
            follows.AddRange(await _followRepository.GetAllByUserIdAsFollowerWithInfoAsync(userId));
            follows.AddRange(await _followRepository.GetAllByUserIdAsFollowedWithInfoAsync(userId));
            if (user != null)
            {
                foreach (var follow in follows)
                {
                    if (follow.FollowId == followId)
                    {
                        await _followRepository.DeleteAsync(follow);
                        return IdentityResult.Success;
                    }
                }
                return IdentityResult.Failed(new NotFoundError("User does not have that type of follow"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));

        }

        public override async Task<IdentityResult> FollowBot(int userId, int followedBotId)
        {
            var follow = new Follow
            {
                UserFollowerId = userId, 
                BotFollowedId = followedBotId,
            };
            await _followRepository.InsertAsync(follow);
            return IdentityResult.Success;
        }

        public override async Task<IdentityResult> FollowUser(int userId, int followedUserId)
        {
            var follow = new Follow
            {
                UserFollowerId = userId,
                UserFollowedId = followedUserId,
            };
            await _followRepository.InsertAsync(follow);
            return IdentityResult.Success;
        }
    }
}
