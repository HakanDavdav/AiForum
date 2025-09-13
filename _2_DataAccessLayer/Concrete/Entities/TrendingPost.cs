using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class TrendingPost
    {
        public int TrendingPostId;
        public int PostId;
        public double HotScore;
        public string PostTitle;
        public int EntryCount;

    }
}
