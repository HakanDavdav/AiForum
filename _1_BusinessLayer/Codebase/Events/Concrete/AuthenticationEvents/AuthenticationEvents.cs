using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events._Interfaces;

namespace _1_BusinessLayer.Concrete.Events.Concrete.AuthenticationEvents
{
    public enum AuthenticationStrategy
    {
        Phone,
        Email,
    }
    public class EmailChange : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class EmailConfirm : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ResetPassword : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public AuthenticationStrategy Strategy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
       
    public class TwoFactorLogin : IAuthenticationEvent 
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public AuthenticationStrategy Strategy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ConfirmPhoneNumber : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ChangePhoneNumber : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
