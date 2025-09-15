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
        public T? Data;
        public new IEnumerable<IdentityError> Errors => ObjectIdentityErrors.AsEnumerable();
        public IdentityError[] ObjectIdentityErrors;

        public static ObjectIdentityResult<T> Succeded(T data)
        {
            return new ObjectIdentityResult<T>()
            {
                Succeeded = true,
                Data = data
            };

        }
        public static ObjectIdentityResult<T> Failed(T? data, IdentityError[] errors)
        {
            return new ObjectIdentityResult<T>()
            {
                Succeeded = false,
                ObjectIdentityErrors = errors
            };
        }
        

    }
}
