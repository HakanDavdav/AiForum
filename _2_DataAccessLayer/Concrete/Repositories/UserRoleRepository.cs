using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Abstractions;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace _2_DataAccessLayer.Concrete.Repositories
{
    public class UserRoleRepository : AbstractUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(UserRole t)
        {
            _context.Roles.Remove(t);
            await _context.SaveChangesAsync();
        }

        public override async Task<List<UserRole>> GetAllAsync()
        {
            IQueryable<UserRole> userRoles = _context.Roles;
            return await userRoles.ToListAsync();
        }

        public override async Task<List<UserRole>> GetAllWithInfoAsync()
        {
            IQueryable<UserRole> userRoles = _context.Roles
                                                     .Include(userRole => userRole.User);
            return await userRoles.ToListAsync();
        }

        public override async Task<UserRole> GetByIdAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            UserRole role = await _context.Roles.FirstOrDefaultAsync(userRole => userRole.Id == id);
                        
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return role;
        }

        public override async Task<UserRole> GetByIdWithInfoAsync(int id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            UserRole role = await _context.Roles.Include(userRole => userRole.User)
                                                .FirstOrDefaultAsync(userRole => userRole.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return role;
        }

        public override async Task InsertAsync(UserRole t)
        {
            await _context.Roles.AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(UserRole t)
        {
            _context.Roles.Attach(t);
            await _context.SaveChangesAsync();
        }
    }
}
