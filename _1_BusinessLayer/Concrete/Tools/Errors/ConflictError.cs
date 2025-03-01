using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.Errors
{
    public class ConflictError : IdentityError
    {
        public ConflictError(string description)
        {
            Code = "Conflicted replicated data error";
            Description = description;
        }
    }
}
