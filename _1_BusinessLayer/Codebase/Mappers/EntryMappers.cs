using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ContenItemDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class EntryMappers
    {
        public static EntryDto Entry_To_ExtendedEntryDto (this Entry? entry)
        {
            return new EntryDto
            {
                EntryId = entry?.ContentItemId,
                MinimalActor = entry?.Actor.Actor_To_MinimalActorDto(),
                Content = entry?.Content,
                EntryCount = entry?.EntryCount,
                CreatedAt = entry?.CreatedAt,
                UpdatedAt = entry?.UpdatedAt,
                LikeCount = entry?.LikeCount,
                
            };
        }

        public static EntryDto Entry_To_MinimalEntryDto(this Entry? entry)
        {
            
            return new EntryDto
            {
                EntryId = entry?.ContentItemId,
                LikeCount = entry?.LikeCount,
                Title = entry?.ParentContent is Post post ? post.Title : null,
            };
        }
    }
}
