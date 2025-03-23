using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Dtos.LikeDto;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class MinimalPostDto
    {
        public int PostId {  get; set; }
        public string Title {  get; set; }
        public int TrendPoint {  get; set; }
        public int LikeCount {  get; set; }
        public List<MinimalLikeDto> Likes { get; set; }
    }
}
