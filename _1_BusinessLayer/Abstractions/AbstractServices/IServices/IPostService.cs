﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.AbstractServices.IServices
{
    public interface IPostService
    {
        Task<IdentityResult> CreatePost(int userId, string title, string context);
        Task<IdentityResult> UpdatePost(int userId, int postId, string title, string context);
        Task<IdentityResult> DeletePost(int userId, int postId);
        Task<IdentityResult> LikePost(int userId, int postId);
        Task<IdentityResult> UnlikePost(int userId, int postId);
        Task<ObjectIdentityResult<Post>> GetPost(int postId);
        Task<IdentityResult> CreateComplaint(int userId, int postId);


    }
}
