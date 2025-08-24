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
    public class LikeService : AbstractLikeService
    {
        public LikeService(AbstractLikeRepository likeRepository, AbstractUserRepository userRepository, 
            AbstractPostRepository postRepository, AbstractEntryRepository entryRepository) : base(likeRepository, userRepository, postRepository, entryRepository)
        {
        }

        public override async Task<IdentityResult> LikeEntry(int entryId, int userId)
        {
            var entry = _entryRepository.GetByIdAsync(entryId);
            if (entry != null)
            {
                var like = new Like
                {
                    EntryId = entryId,
                    UserId = userId
                };
                await _likeRepository.ManuallyInsertAsync(like);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("Entry not found"));
        }

        public override async Task<IdentityResult> LikePost(int postId, int userId)
        {
            var entry = _postRepository.GetByIdAsync(postId);
            if (entry != null)
            {
                var like = new Like
                {
                    PostId = postId,
                    UserId = userId
                };
                await _likeRepository.ManuallyInsertAsync(like);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("Entry not found"));
        }

        public override async Task<IdentityResult> Unlike(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdAsync(likeId);
            if (like.UserId == userId)
            {
                await _likeRepository.DeleteAsync(like);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new UnauthorizedError("Unauthorized"));
        }


    }
}
