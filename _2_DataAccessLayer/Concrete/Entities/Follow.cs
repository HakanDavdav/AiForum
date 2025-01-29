using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Follow
    {
        public int followId {  get; set; }



        public int? followeeId { get; set; }
        public User followee { get; set; }


        public int? followedId { get; set; }
        public User followed { get; set; }

    }
}
