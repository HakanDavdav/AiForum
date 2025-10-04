using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Events._Interfaces
{
    public interface IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
