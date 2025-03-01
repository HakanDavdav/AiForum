using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.Errors
{
    public class UnauthorizedError : IdentityError
    {
        public UnauthorizedError(string description)
        {
            Code = "Unauthorized";
            Description = description;
        }
    }
}
