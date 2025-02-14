using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Abstractions.SideServices
{
    public abstract class AbstractAuthenticationService
    {
        public abstract string GenerateJwtToken(User user);
    }
}
