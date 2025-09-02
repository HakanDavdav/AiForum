using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Like 
    {
        public int LikeId {  get; set; }
        public DateTime DateTime { get; set; }



        public int? PostId { get; set; }
        public Post? Post { get; set; }


        public int? EntryId { get; set; }
        public Entry? Entry { get; set; }


        public int? OwnerUserId { get; set; }
        public User? OwnerUser { get; set; }
        public int? OwnerBotId { get; set; }
        public Bot? OwnerBot { get; set; }
    } 
        
 }
