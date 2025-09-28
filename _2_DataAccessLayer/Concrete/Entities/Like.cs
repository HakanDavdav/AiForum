using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Enums;
using Azure.Core;
using static _2_DataAccessLayer.Concrete.Enums.ContentType;

namespace _2_DataAccessLayer.Concrete.Entities
{
    public class Like 
    {
        public Guid LikeId {  get; set; }
        public DateTime DateTime { get; set; }

        public Guid? ContentId { get; set; }
        public ContentTypes ContentType { get; set; }
        public Post? Post { get; set; }
        public Entry? Entry { get; set; }


        public Guid? OwnerId { get; set; }
        public ActorTypes OwnerActorType { get; set; }
        public User? OwnerUser { get; set; }
        public Bot? OwnerBot { get; set; }
    } 
        
 }
