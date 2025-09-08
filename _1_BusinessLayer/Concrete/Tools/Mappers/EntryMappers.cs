using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class EntryMappers
    {
       public static EntryPostDto Entry_To_EntryPostDto(this Entry entry)
        {
            var minimalBotDto = entry.OwnerBot.Bot_To_MinimalBotDto();
            var minimalUserDto = entry.OwnerUser.User_To_MinimalUserDto();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in entry.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return new EntryPostDto
            {
                Context = entry.Context,
                DateTime = entry.DateTime,
                LikeCount = entry.LikeCount,
                EntryId = entry.EntryId,
                Bot = minimalBotDto,
                User = minimalUserDto,
                Likes = minimalLikeDtos,
            };
        }

        public static EntryProfileDto Entry_To_EntryProfileDto(this Entry entry)
        {
            var minimalBotDto = entry.OwnerBot.Bot_To_MinimalBotDto();
            var minimalUserDto = entry.OwnerUser.User_To_MinimalUserDto();
            var minimalPostDto = entry.Post.Post_To_MinimalPostDto();
            List<MinimalLikeDto> minimalLikeDtos = new List<MinimalLikeDto>();
            foreach (var like in entry.Likes ?? new List<Like>())
            {
                minimalLikeDtos.Add(like.Like_To_MinimalLikeDto());
            }
            return new EntryProfileDto
            {
                Context = entry.Context,
                DateTime = entry.DateTime,               
                EntryId = entry.EntryId,
                LikeCount = entry.LikeCount,
                Bot = minimalBotDto,
                User = minimalUserDto,
                Likes = minimalLikeDtos,
                Post = minimalPostDto,

            };
        }

        public static Entry CreateEntryDto_To_Entry(this CreateEntryDto entry)
        {
            return new Entry
            {
                Context = entry.Context,
                DateTime = entry.DateTime,
                PostId = entry.PostId,             
            };
        }

        public static Entry Update___EditEntryDto_To_Entry(this EditEntryDto editEntryDto, Entry entry)
        {
            entry.Context = editEntryDto.Context;
            entry.DateTime = editEntryDto.DateTime;
            entry.EntryId = editEntryDto.EntryId;
            return entry;
        }
    }
}
