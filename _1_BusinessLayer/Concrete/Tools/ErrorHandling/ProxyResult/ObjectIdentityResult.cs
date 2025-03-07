using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.ErrorHandling.ProxyResult
{
    public class ObjectIdentityResult<T> : IdentityResult
    {
        IdentityResult? IdentityResult;
        T? Data;

        public static ObjectIdentityResult<T> Succeded(T data)
        {
            return new ObjectIdentityResult<T>()
            {
                IdentityResult = Success,
                Data = data
            };

        }
        public static ObjectIdentityResult<T> Failed(T? data, IdentityError[] errors)
        {
            return new ObjectIdentityResult<T>()
            {
                IdentityResult = Failed(errors),
            };
        }
    }
}
