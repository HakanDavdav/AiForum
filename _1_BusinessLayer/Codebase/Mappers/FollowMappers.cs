using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.OtherDtos.OutputDtos;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class FollowMappers
    {

        public static FollowDto Follow_To_FollowDto(this Follow? follow)
        {
            return new FollowDto
            {
                CreatedAt = follow?.CreatedAt,
                Follower = follow?.FollowerActor.Actor_To_MinimalActorDto(),
                Followed = follow?.FollowedActor.Actor_To_MinimalActorDto()
            };
        }

    }
}
