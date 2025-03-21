using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IFollowService
    {
        //Self-Authorization requirement
        Task<IdentityResult> FollowUser(int userId,int followedUserId);
        //Self-Authorization requirement
        Task<IdentityResult> FollowBot(int userId, int followedBotId);
        //Self-Authorization requirement
        Task<IdentityResult> DeleteFollow(int userId, int followId);

    }
}
