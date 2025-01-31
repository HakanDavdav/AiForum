using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Dtos;

namespace _1_BusinessLayer.Abstractions
{
    public interface IUserService
    {
        public UserRegisterDto GetUserByID(int id);
    }
}
