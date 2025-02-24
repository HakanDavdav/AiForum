using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Errors
{
    public class ValidationError : IdentityError
    {
        public ValidationError(string description)
        {
            Code = "Mail Validation Error";
            Description = description;
        }
    }
}
