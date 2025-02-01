using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_BusinessLayer.Concrete.Dtos
{
    public class UserRegisterDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string confirmCode { get; set; }
    }
}
