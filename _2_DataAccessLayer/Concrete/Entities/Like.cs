using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;
using Azure.Core;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Like 
    {
        public int LikeId {  get; set; }
        public DateTime DateTime { get; set; }

        public Guid? PostId { get; set; }
        public Post? Post { get; set; }
        public Guid? EntryId { get; set; }  
        public Entry? Entry { get; set; }


        public Guid? ActorOwnerId { get; set; }
        public Actor? ActorOwner { get; set; }

    } 
        
 }
