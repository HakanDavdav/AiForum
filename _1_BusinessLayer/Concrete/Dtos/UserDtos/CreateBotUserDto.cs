using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class CreateBotUserDto
    {
        public string ProfileName { get; set; }
        public string ImageUrl { get; set; }
        public string City { get; set; }
        public string Personality { get; set; }
    }
}
