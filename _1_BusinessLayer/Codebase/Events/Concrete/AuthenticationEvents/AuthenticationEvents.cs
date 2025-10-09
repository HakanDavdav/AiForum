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
    public class EmailChangeEvent : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class EmailConfirmEvent : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ResetPasswordEvent : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public AuthenticationStrategy Strategy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
       
    public class TwoFactorLoginEvent : IAuthenticationEvent 
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public AuthenticationStrategy Strategy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ConfirmPhoneNumberEvent : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ChangePhoneNumberEvent : IAuthenticationEvent
    {
        public Guid ActorId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
