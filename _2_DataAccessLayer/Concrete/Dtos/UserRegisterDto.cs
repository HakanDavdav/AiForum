using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Dtos
{
    public class UserRegisterDto
    {

        public string email { get; set; }
        public string passwordHash { get; set; }
        

    }
}
