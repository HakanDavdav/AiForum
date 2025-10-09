using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Codebase.Dtos.TribeDtos.Output;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Codebase.Mappers
{
    public static class TribeMappers
    {
        public static TribeDto Tribe_To_ExtendedTribeDto(this Tribe? tribe)
        {
            List<Tribe> rivalTribes = new List<Tribe>();
            var tribeMembers = tribe!.TribeMemberships!.Select(tm => tm.Actor).ToList();
            rivalTribes.Concat(tribe!.OutgoingRivalries!.Select(tr => tr.TargetTribe).ToList());
            rivalTribes.Concat(tribe!.IncomingRivalries!.Select(tr => tr.InitiatingTribe).ToList());
            List<TribeDto> rivalTribeDtos = new List<TribeDto>();
            foreach (var rivalTribe in rivalTribes)
            {
                rivalTribeDtos.Add(rivalTribe.Tribe_To_MinimalTribeDto());
            }
            return new TribeDto
            {
                TribeId = tribe?.TribeId,
                CreatedAt = tribe?.CreatedAt,
                ImageUrl = tribe?.ImageUrl, 
                TribeName = tribe?.TribeName,
                MemberCount = tribe?.MemberCount,
                Mission = tribe?.Mission,
                TribePoint = tribe?.TribePoint,
                RivalTribes = rivalTribeDtos,
                
            };
        }

        public static TribeDto Tribe_To_MinimalTribeDto(this Tribe? tribe)
        {
            return new TribeDto
            {
                TribeId = tribe?.TribeId,
                CreatedAt = tribe?.CreatedAt,
                ImageUrl = tribe?.ImageUrl,
                TribeName = tribe?.TribeName,
                MemberCount = tribe?.MemberCount,
                TribePoint = tribe?.TribePoint
            };
        }

    }
}
