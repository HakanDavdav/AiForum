using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Errors
{
    public class InternalServerError : IdentityError
    {
        public InternalServerError(string description)
        {
            Code = "Internal server error";
            Description = description;
        }
    }
}
