using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.LikeDto
{
    public class MinimalLikeDto
    {
        public int LikerId {  get; set; }
        public string LikerProfileName { get; set; }
        public string LikerImageUrl { get; set; }

    }
}
