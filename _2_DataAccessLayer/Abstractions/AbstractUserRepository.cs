using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions.Generic;
using _2_DataAccessLayer.Concrete;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _2_DataAccessLayer.Abstractions
{
    public abstract class AbstractUserRepository : AbstractGenericBaseRepository<User>
    {
        protected readonly UserManager<User> _userManager;
        protected AbstractUserRepository(ApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }
        public abstract Task<User> GetByEmailAsync(string email);
        public abstract Task<User> GetByUsernameAsync(string name);
        public abstract Task<List<String>> GetUserRolesAsync(User user);
        public abstract Task<User> SearchUserAsync(string query);
    }
}
