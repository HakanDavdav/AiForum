using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRoleRepository : AbstractUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Delete(UserRole t)
        {
            _context.Roles.Remove(t);
        }

        public override List<UserRole> GetAll()
        {
            IQueryable<UserRole> userRoles = _context.Roles;
            return userRoles.ToList();
        }

        public override UserRole GetById(int id)
        {
            UserRole role = _context.Roles.Find(id);
            return role;
        }

        public override void Insert(UserRole t)
        {
            _context.Roles.Add(t);
        }

        public override void Update(UserRole t)
        {
            _context.Roles.Attach(t);
            //make changes
        }
    }
}
