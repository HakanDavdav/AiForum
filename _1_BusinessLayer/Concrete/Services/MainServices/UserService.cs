using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions.MainServices;
using _1_BusinessLayer.Abstractions.SideServices;
using _1_BusinessLayer.Concrete.Services.SideServices;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Services.MainServices
{
    public class UserService : AbstractUserService
    {
        public UserService(AbstractAuthenticationService authenticationService, AbstractAuthorizationService authorizationService, 
            AbstractMailService mailService, AbstractUserRepository userRepository) : base(authenticationService, authorizationService, mailService, userRepository)
        {
        }

        public override void ChangePassword(string url)
        {
            throw new NotImplementedException();
        }

        public override void ChangePP(int id)
        {
            throw new NotImplementedException();
        }

        public override void ChangeUsername(string url)
        {
            throw new NotImplementedException();
        }

        public override User getByName(string name)
        {
            throw new NotImplementedException();
        }

        public override void Login(User user)
        {
            throw new NotImplementedException();
        }

        public override void Register(string email, string password)
        {
               
        }

        public override void TDelete(User t)
        {
            throw new NotImplementedException();
        }

        public override List<User> TGetAll()
        {
            throw new NotImplementedException();
        }

        public override User TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void TInsert(User t)
        {
            throw new NotImplementedException();
        }

        public override void TUpdate(User t)
        {
            throw new NotImplementedException();
        }
    }
}
