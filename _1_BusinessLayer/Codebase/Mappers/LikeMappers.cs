using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class LikeMappers
    {
        public static LikeDto Like_To_ExtendedLikeDto(this Like? like)
        {
            return new LikeDto
            {
                CreatedAt = like?.CreatedAt,
                ReactionType = like?.ReactionType,
                Actor = like?.Actor.Actor_To_MinimalActorDto(),
                Entry = like?.ContentItem is Entry entry ? entry.Entry_To_MinimalEntryDto() : null,
                Post = like?.ContentItem is Post post ? post.Post_To_ExtendedPostDto() : null

            };

        }

        public static LikeDto Like_To_MinimalLikeDto(this Like? like)
        {
            return new LikeDto
            {
                CreatedAt = like?.CreatedAt,
                ReactionType = like?.ReactionType,
                Actor = like?.Actor.Actor_To_MinimalActorDto()
            };
        }
    }
}
