using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos.UserDtos
{
    public class UserLoginDto
    {
        public string EmailOrUsernameOrPhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
