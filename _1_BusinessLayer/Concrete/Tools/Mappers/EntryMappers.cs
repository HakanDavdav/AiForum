using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotActivityDtos;
using _1_BusinessLayer.Concrete.Dtos.EntryDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class EntryMappers
    {
        public static Entry CreateEntryDto_To_Entry(this CreateEntryDto createEntryDto,User user)
        {
            return new Entry
            {
                UserId = user.Id,
                PostId = createEntryDto.PostId,
                DateTime = createEntryDto.DateTime,
            };
        }

        public static EntryDto Entry_To_EntryDto(this Entry entry)
        {
            return new EntryDto
            {
                EntryId = entry.PostId,
                Bot = entry.Bot,
                DateTime = entry.DateTime,
                Context = entry.Context,
                Likes = entry.Likes,
                User = entry.User,             
            };
        }

        public static EntryProfileDto Entry_To_EntryProfileDto(this Entry entry)
        {
            return new EntryProfileDto
            {
                EntryId = entry.PostId,
                Bot = entry.Bot,
                DateTime = entry.DateTime,
                Context = entry.Context,
                Likes = entry.Likes,
                Post = entry.Post,
                User = entry.User,
            };
        }

        public static Entry Update___EditEntryDto_To_Entry(this EditEntryDto editEntryDto, Entry entry)
        {
            entry.EntryId = editEntryDto.EntryId;
            entry.Context = editEntryDto.Context;
            entry.DateTime = editEntryDto.DateTime;
            return entry;
        }
    }
}
