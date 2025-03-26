using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractUserRepository : AbstractGenericBaseRepository<User>
    {
        protected AbstractUserRepository(ApplicationDbContext context, ILogger<User> logger) : base(context, logger)
        {
        }

        public abstract Task<User> GetByEmailAsync(string email);
        public abstract Task<User> GetByUsernameAsync(string name);
        public abstract Task<User> GetByPhoneNumberAsync(string phoneNumber);
        public abstract Task<User> GetByProfileNameAsync(string profileName);
        public abstract Task<List<User>> GetRandomUsers(int number);

    }
}
