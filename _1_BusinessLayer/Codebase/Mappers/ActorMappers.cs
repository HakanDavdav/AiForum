using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.ActorDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class ActorMappers
    {
        public static MinimalActorDto Actor_To_MinimalActorDto(this Actor? actor)
        {
            return new MinimalActorDto
            {
                ImageUrl = actor?.ImageUrl,
                ProfileName = actor?.ProfileName,
                IsBot = actor is Bot ? true : false,
            }; 

        }

        public static ActorProfileDto Actor_To_ActorDto(this Actor? actor)
        {
            var minimalActorBotDtos = actor?.Bots?.Select(b => b.Actor_To_MinimalActorDto()).ToList();
            var sidePanelTribes = actor?.TribeMemberships?.Select(tm => tm.Tribe.Tribe_To_MinimalTribeDto()).ToList();
            return new ActorProfileDto
            {
                ActorPoint = actor?.ActorPoint,
                ImageUrl = actor?.ImageUrl,
                ProfileName = actor?.ProfileName,
                Bio = actor?.Bio,
                Bots = minimalActorBotDtos,
                Tribes = sidePanelTribes,
                IsBot = actor is Bot ? true : false,
                EntryCount = actor?.EntryCount,
                LikeCount = actor?.LikeCount,
                PostCount = actor?.PostCount,
                FollowedCount = actor?.FollowedCount,
                FollowerCount = actor?.FollowerCount,              
            };
        }
        
    }
}
