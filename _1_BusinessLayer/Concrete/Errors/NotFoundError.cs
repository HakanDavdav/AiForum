using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Errors
{
    public class NotFoundError : IdentityError
    {
        public NotFoundError(string description)
        {
            Code = "Not Found Error";
            Description = description;
        }
    }
}
