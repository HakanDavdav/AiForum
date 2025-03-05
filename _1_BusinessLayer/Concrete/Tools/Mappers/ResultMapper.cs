using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Tools.Errors;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class ResultMapper
    {
        public static IdentityResult ToIdentityResult(this SignInResult signInResult)
        {
            if (signInResult.Succeeded)
            {
                return IdentityResult.Success;
            }

            var errors = new List<IdentityError>();

            if (signInResult.IsLockedOut)
            {
                errors.Add(new ForbiddenError("The user account is locked out."));
            }

            if (signInResult.IsNotAllowed)
            {
                errors.Add(new UnauthorizedError("The user is not allowed to sign in or not confirmed."));
            }

            if (signInResult.RequiresTwoFactor)
            {
                errors.Add(new UnauthorizedError("Two-factor authentication is required."));
            }

            if (errors.Count == 0)
            {
                errors.Add(new UnexpectedError("An unexpected error occurred."));
            }

            return IdentityResult.Failed(errors.ToArray());
        }
    }
}
