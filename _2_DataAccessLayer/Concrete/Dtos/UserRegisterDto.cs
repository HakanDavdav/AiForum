using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DataAccessLayer.Concrete.Dtos
{
    public class UserRegisterDto
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
