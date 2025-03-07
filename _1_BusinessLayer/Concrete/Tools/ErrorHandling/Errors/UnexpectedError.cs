using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.ErrorHandling.Errors
{
    public class UnexpectedError : IdentityError
    {
        public UnexpectedError(string description)
        {
            Code = "Unexpected Error";
            Description = description;
        }
    }
}
