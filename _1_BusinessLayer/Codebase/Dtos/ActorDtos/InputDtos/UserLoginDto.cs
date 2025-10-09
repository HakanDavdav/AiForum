using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Codebase.Dtos.ActorDtos.InputDtos
{
    public class UserLoginDto
    {

        public string? UsernameOrEmailOrPhoneNumber { get; set; }

        public string? Password { get; set; }
    }
}
