using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.PostDtos
{
    public class MinimalPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public int? EntryCount { get; set; }
    }
}
