using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Events.Interfaces
{
    public interface ISocialEvent
    {
        public Guid CreatorActorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
