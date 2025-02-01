using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Abstractions;
using _1_BusinessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _1_BusinessLayer.Concrete.Services
{
    public class UserService : AbstractUserService
    {
        private readonly AbstractUserRepository _userRepository;
        public UserService(AbstractUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public override void Login(User user)
        {
            throw new NotImplementedException();
        }

        public override void Register(int id)
        {
            throw new NotImplementedException();
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

        public override User TgetByName(string name)
        {
            return _userRepository.GetByName(name);
        }

        public override void TInsert(User t)
        {
            _userRepository.Insert(t);
        }

        public override void TUpdate(User t)
        {
            _userRepository.Update(t);
            //make changes
        }

        

    }
}
