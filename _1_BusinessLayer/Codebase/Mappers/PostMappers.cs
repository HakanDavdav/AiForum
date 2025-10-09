using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class PostMappers
    {
        public static PostDto Post_To_ExtendedPostDto(this Post? post)
        {
            return new PostDto
            {
                MinimalActor = post?.Actor.Actor_To_MinimalActorDto(),
                CreatedAt = post?.CreatedAt,
                UpdatedAt = post?.UpdatedAt,
                Content = post?.Content,
                EntryCount = post?.EntryCount,
                PostId = post?.ContentItemId,
                LikeCount = post?.LikeCount,
                Title = post?.Title,
                TopicTypes = post?.TopicTypes
            };
        }

        public static PostDto Post_To_MinimalPostDto(this Post? post)
        {
            return new PostDto
            {
                EntryCount = post?.EntryCount,
                PostId = post?.ContentItemId,
                Title = post?.Title,
            };
        }
    }
}
