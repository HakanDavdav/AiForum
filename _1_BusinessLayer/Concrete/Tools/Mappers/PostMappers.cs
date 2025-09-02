using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class PostMappers
    {

        public static MinimalPostDto Post_To_MinimalPostDto(this Post post)
        {
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in post.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return new MinimalPostDto
            {
                Title = post.Title,
                PostId = post.PostId,
                EntryCount = post.EntryCount,
            };
        }

        public static PostProfileDto Post_To_PostProfileDto(this Post post)
        {
            var minimalUser = post.OwnerUser.User_To_MinimalUserDto();
            var minimalBot = post.OwnerBot.Bot_To_MinimalBotDto();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in post.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return new PostProfileDto
            {
                Title = post.Title,
                PostId = post.PostId,
                Context = post.Context,
                DateTime = post.DateTime,
                Likes = minimalLikeDtos,
                LikeCount = post.LikeCount,
                Bot = minimalBot,
                User = minimalUser,
            };
        }

        public static PostDto Post_To_PostDto(this Post post)
        {
            var minimalUser = post.OwnerUser.User_To_MinimalUserDto();
            var minimalBot = post.OwnerBot.Bot_To_MinimalBotDto();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            List<EntryPostDto> entryPostDtos = new List<EntryPostDto>();
            foreach (var entry in post.Entries ?? new List<Entry>())
            {
                entryPostDtos.Add(entry.Entry_To_EntryPostDto());
            }
            foreach (var like in post.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return new PostDto
            {
                Title = post.Title,
                PostId = post.PostId,
                Context = post.Context,
                DateTime = post.DateTime,
                Bot = minimalBot,
                User = minimalUser,
                Entries = entryPostDtos,
                LikeCount = post.LikeCount,
                EntryCount = post.EntryCount,
                Likes = minimalLikeDtos
            };

        }

        public static Post CreatePostDto_To_Post(this CreatePostDto createPostDto, int userId)
        {
            return new Post
            {
                OwnerUserId = userId,
                Context = createPostDto.Context,
                DateTime = createPostDto.DateTime,
                Title = createPostDto.Title,
            };
        }


        public static Post Update___EditPostDto_To_Post(this EditPostDto editPostDto,Post post)
        {
            post.Context = editPostDto.Context;
            post.DateTime = editPostDto.DateTime;
            post.Title = editPostDto.Title;
            return post;
        }
    }
}
