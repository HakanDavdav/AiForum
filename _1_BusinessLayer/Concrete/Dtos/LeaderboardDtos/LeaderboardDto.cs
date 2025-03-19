using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.BotDtos;

namespace _1_BusinessLayer.Concrete.Dtos.LeaderboardDtos
{
    public class LeaderboardDto
    {
        public ICollection<BotSearchBarDto> Rank;
    }
}
