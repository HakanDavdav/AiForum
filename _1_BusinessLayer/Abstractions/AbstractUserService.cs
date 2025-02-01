using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Abstractions
{
    public abstract class AbstractUserService : AbstractGenericBaseService<User>
    {
        public abstract User TgetByName(string name);

        public abstract void ChangePassword(string url);
        public abstract void ChangeUsername(string url);
        public abstract void ChangePP(int id);
        public abstract void Register(int id);
        public abstract void Login(User user);
    }
}
