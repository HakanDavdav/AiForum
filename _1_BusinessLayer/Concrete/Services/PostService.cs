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
                await _postRepository.InsertAsync(post);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new NotFoundError("User not found"));
        }

        public override async Task<IdentityResult> DeletePost(int userId, int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                if (post.UserId == userId)
                {
                    await _postRepository.DeleteAsync(post);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not have that kind of post:)"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));
        }

        public override async Task<IdentityResult> EditPost(int userId, EditPostDto editPostDto)
        {
            var post = await _postRepository.GetByIdAsync(editPostDto.PostId);
            if (post != null)
            {
                if (post.UserId == userId)
                {
                    post = editPostDto.Update___EditPostDto_To_Post(post);
                    await _postRepository.UpdateAsync(post);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new UnauthorizedError("User does not have that kind of post:)"));
            }
            return IdentityResult.Failed(new NotFoundError("Post not found"));
        }

        public override Task<ObjectIdentityResult<int>> GetEntryCountByPost(int postId)
        {
            throw new NotImplementedException();
        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetMostLikedPosts(string postPerPagePreference,DateTime date)
        {
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<Post> posts = await _postRepository.GetAllWithCustomSearch(q =>
                q.Where(p => p.DateTime.Date == date.Date) 
                .OrderByDescending(p => p.Likes) 
                .Take(int.Parse(postPerPagePreference)) 
                 );
            foreach (var post in posts)
            {
                minimalPostDtos.Add(post.Post_To_MinimalPostDto());
            }
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }

        public override async Task<ObjectIdentityResult<PostDto>> GetPostAsync(int postId, int page, string? entryPerPagePreference = "10")
        {            
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                var entryCount = await _entryRepository.GetEntryCountByPostIdAsync(postId);
                var intEntryPerPagePreference = int.Parse(entryPerPagePreference);
                var user = await _userRepository.GetByIdAsync((int)post.UserId);
                var bot = await _botRepository.GetByIdAsync((int)post.BotId);
                var entries = await _entryRepository.GetAllByPostId
                    (postId,page*intEntryPerPagePreference - intEntryPerPagePreference, page* intEntryPerPagePreference);
                var likes = await _likeRepository.GetAllByPostIdAsync(postId);
                foreach (var postLike in likes)
                {
                    postLike.User = await _userRepository.GetByIdAsync((int)post.UserId);
                    postLike.Bot = await _botRepository.GetByIdAsync((int)(post.BotId));
                }
                foreach (var entry in entries)
                {
                    entry.Likes = await _likeRepository.GetAllByEntryIdAsync(entry.EntryId);
                    entry.User = await _userRepository.GetByIdAsync((int)entry.UserId);
                    entry.Bot = await _botRepository.GetByIdAsync(((int)entry.BotId));

                    foreach (var entryLike in entry.Likes)
                    {
                        entryLike.User = await _userRepository.GetByIdAsync((int)entryLike.UserId);
                        entryLike.Bot = await _botRepository.GetByIdAsync(((int)entryLike.BotId));
                    }
                }
                post.Bot = bot;
                post.User = user;
                post.Likes = likes;
                post.Entries = entries;
                var postDto = post.Post_To_PostDto();
                postDto.EntryCount = entryCount;
                return ObjectIdentityResult<PostDto>.Succeded(postDto);
            }
            return ObjectIdentityResult<PostDto>.Failed(null, new IdentityError[] { new NotFoundError("Post not found") });

        }

        public override async Task<ObjectIdentityResult<List<MinimalPostDto>>> GetTrendingPosts(string postPerPagePreference,DateTime date)
        {
            List<MinimalPostDto> minimalPostDtos = new List<MinimalPostDto>();
            List<Post> posts = await _postRepository.GetAllWithCustomSearch(q =>
                     q.Where(p => p.DateTime.Date > date.AddDays(-3)) // Son 3 gün içindeki postları al
                     .OrderByDescending(p => p.Likes) // Beğeni sayısına göre azalan sırala
                     .Take(int.Parse(postPerPagePreference)) // Opsiyonel: En çok beğeni alan ilk 10 postu getir
                      );
            foreach (var post in posts)
            {
                minimalPostDtos.Add(post.Post_To_MinimalPostDto());
            }
            return ObjectIdentityResult<List<MinimalPostDto>>.Succeded(minimalPostDtos);
        }
    }
}
