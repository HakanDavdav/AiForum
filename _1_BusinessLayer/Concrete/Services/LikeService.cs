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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var entry = await _entryRepository.GetByIdAsync(entryId);
            if (entry != null)
            {
                entry.Likes.Add(new Like
                {
                    EntryId = entryId,
                    UserId = userId
                });
                entry.LikeCount += 1;
                await _likeRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("Entry not found"));
        }

        public override async Task<IdentityResult> LikePost(int postId, int userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                post.Likes.Add(new Like
                {
                    PostId = postId,
                    UserId = userId
                });
                post.LikeCount += 1;
                await _likeRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("Entry not found"));
        }

        public override async Task<IdentityResult> UnlikeEntry(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdAsync(likeId);
            if (like != null)
            {
                 var entry = await _entryRepository.GetByIdAsync(like.EntryId.Value);
                 if (entry != null)
                 {
                    if (entry.EntryId == like.EntryId)
                    {
                        await _likeRepository.DeleteAsync(like);
                        entry.LikeCount--;
                        await _likeRepository.SaveChangesAsync();
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("Entry doesen't have that like "));
                }
                 return IdentityResult.Failed(new UnauthorizedError("Entry not found "));
            }
            return IdentityResult.Failed(new NotFoundError("Like not found"));
        }

        public override async Task<IdentityResult> UnlikePost(int userId, int likeId)
        {
            var like = await _likeRepository.GetByIdAsync(likeId);
            if (like != null)
            {
                 var post = await _postRepository.GetByIdAsync(like.PostId.Value);      
                 if (post != null)
                 {
                    if ( post.PostId == like.PostId) 
                    { 
                     await _likeRepository.DeleteAsync(like);
                     post.LikeCount--;
                     await _likeRepository.SaveChangesAsync();
                     return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("Post doesen't have that like "));
                }
              return IdentityResult.Failed(new NotFoundError("Post not found"));
            }
            return IdentityResult.Failed(new NotFoundError("Like not found"));
        }
    }
}
