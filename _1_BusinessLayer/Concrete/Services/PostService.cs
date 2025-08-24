using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.AbstractServices;
using _1_BusinessLayer.Abstractions.AbstractServices.IServices;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;
using _1_BusinessLayer.Concrete.Dtos.NotificationDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors;
using _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult;
using _1_BusinessLayer.Concrete.Tools.Mappers;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Services
{
    public class PostService : AbstractPostService
    {
        public PostService(AbstractPostRepository postRepository, AbstractUserRepository userRepository, 
            AbstractEntryRepository entryRepository, AbstractLikeRepository likeRepository) : base(postRepository, userRepository, entryRepository, likeRepository)
        {
        }

        public override async Task<IdentityResult> CreatePost(int userId, CreatePostDto createPostDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = createPostDto.CreatePostDto_To_Post(userId);
                user.Posts.Add(post);
                await _postRepository.SaveChangesAsync();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DeletePost(int userId, int postId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = await _postRepository.GetByIdAsync(postId);
                if (post != null && post.UserId == userId)
                {
                    user.Posts.Remove(post);
                    await _postRepository.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not have that kind of post:)"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));
        }

        public override async Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                var post = await _postRepository.GetByIdAsync(editPostDto.PostId);
                if (post != null)
                {
                    if (post.UserId == userId)
                    {
                        post = editPostDto.Update___EditPostDto_To_Post(post);
                        await _postRepository.SaveChangesAsync();
                        return IdentityResult.Success;
                    }
                    return IdentityResult.Failed(new UnauthorizedError("User does not have that kind of post:)"));
                }
                return IdentityResult.Failed(new NotFoundError("Post not found"));
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(DateTime date, int postPerPagePreference)
        {
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<Post> posts = await _postRepository.GetWithCustomSearchAsync(q =>
                q.Where(p => p.DateTime.Date == date.Date) 
                .OrderByDescending(p => p.Likes) 
                .Take(postPerPagePreference) 
                 );
            foreach (var post in posts)
            {
                minimalPostDtos.Add(post.Post_To_MinimalPostDto());
            }
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(int postPerPagePreference, DateTime date)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, int page, string? entryPerPagePreference = "10")
        {
            var intEntryPerPagePreference = int.Parse(entryPerPagePreference);
            var listPost = await _postRepository.GetWithCustomSearchAsync(query => query.Where(post =>post.PostId == postId)
            .Select(post => new Post
            {
                PostId = post.PostId,
                UserId = post.UserId,
                BotId = post.BotId,
                DateTime = post.DateTime,
                Title = post.Title,
                Context = post.Context,
                Likes = post.Likes.Select(like => new Like
                {
                    LikeId = like.LikeId,
                    UserId = like.UserId,
                    BotId = like.BotId,
                    PostId = like.PostId,
                    EntryId = like.EntryId,
                    User = like.User,
                    Bot = like.Bot,
                    DateTime = like.DateTime            
                }).ToList(),
                Entries = post.Entries.Skip((page - 1) * intEntryPerPagePreference).Take(intEntryPerPagePreference).Select(entry => new Entry
                {
                    EntryId = entry.EntryId,
                    UserId = entry.UserId,
                    BotId = entry.BotId,
                    PostId = entry.PostId,
                    DateTime = entry.DateTime,
                    Context = entry.Context,
                    Likes = entry.Likes.Select(like => new Like
                    {
                        LikeId = like.LikeId,
                        UserId = like.UserId,
                        BotId = like.BotId,
                        PostId = like.PostId,
                        EntryId = like.EntryId,
                        User = like.User,
                        Bot = like.Bot,
                        DateTime = like.DateTime
                    }).ToList(),
                    User = entry.User,
                    Bot = entry.Bot
                }).ToList()
            }
            )
            );
            var post = listPost.FirstOrDefault();
            if (post != null)
            {
                var entryCount = await _postRepository.GetEntryCountOfPost(postId);
                var postDto = post.Post_To_PostDto();
                postDto.EntryCount = entryCount;
                return ObjectIdentityResult<PostDto>.Succeded(postDto);
            }
            return ObjectIdentityResult<PostDto>.Failed(null, new IdentityError[] { new NotFoundError("Post not found") });

        }

        public override Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, int page, int entryPerPagePreference)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(string postPerPagePreference,DateTime date)
        {
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<Post> posts = await _postRepository.GetWithCustomSearchAsync(q =>
                     q.Where(p => p.DateTime.Date > date.AddDays(-3)) 
                     .OrderByDescending(p => p.TrendPoint) 
                     .Take(int.Parse(postPerPagePreference)) 
                      );
            foreach (var post in posts)
            {
                minimalPostDtos.Add(post.Post_To_MinimalPostDto());
            }
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(int entryPerPagePreference, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
