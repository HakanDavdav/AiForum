using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.PostDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class PostMappers
    {
        public static Post CreatePostDto_To_Post(this CreatePostDto createPostDto,User user)
        {
            return new Post
            {
                Context = createPostDto.Context,
                Title = createPostDto.Title,
                DateTime = createPostDto.DateTime,    
                UserId = user.Id,
            };
        }


        public static PostDto Post_To_PostDto(this Post post)
        {
            return new PostDto
            {
                Bot = post.Bot,
                Context = post.Context,
                DateTime = post.DateTime,
                Entries = post.Entries,
                Likes = post.Likes,
                PostId = post.PostId,
                Title = post.Title,
                TrendPoint = post.TrendPoint,
                User = post.User,
            };
        }

        public static PostProfileDto Post_To_PostProfileDto(this Post post)
        {
            return new PostProfileDto
            {
                Bot = post.Bot,
                Context = post.Context,
                DateTime = post.DateTime,
                Likes = post.Likes,
                PostId = post.PostId,
                Title = post.Title,
                TrendPoint = post.TrendPoint,
                User = post.User,
            };
        }

        public static PostSearchBarDto Post_To_PostSearchBarDto(this Post post)
        {
            return new PostSearchBarDto
            {
                Likes = post.Likes,
                PostId = post.PostId,
                Title = post.Title,               
            };
        }

        public static SidePostDto Post_To_SidePostDto(this Post post)
        {
            return new SidePostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                TrendPoint = post.TrendPoint    
            };
        }

        public static Post Update___EditPostDto_To_Post(this EditPostDto editPostDto, Post post)
        {
             post.Context = editPostDto.Context;
             post.DateTime = editPostDto.DateTime;
             post.Title = editPostDto.Title;
             return post;
        }


    }
}
