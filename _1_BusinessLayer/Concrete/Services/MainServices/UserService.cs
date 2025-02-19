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
using _2_DataAccessLayer.Concrete.Repositories;

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

        public override User TgetByName(string name)
        {
            return _userRepository.GetByName(name);
        }

        public override void Login(User user)
        {
            throw new NotImplementedException();
        }

        public override void Register(string email, string password)
        {
            new User()
            {
                Email = email,
                PasswordHash = password
            };

            _userRepository.Insert(t);
            _authenticationService.GenerateJwtToken();   
        }

        public override void TDelete(User t)
        {
            _userRepository.Delete(t);
        }

        public override List<User> TGetAll()
        {
            return _userRepository.GetAll();    
        }

        public override User TGetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public override void TInsert(User t)
        {
            _userRepository.Insert(t);
        }

        public override void TUpdate(User t)
        {
            _userRepository.Update(t);
        }

    }
}
