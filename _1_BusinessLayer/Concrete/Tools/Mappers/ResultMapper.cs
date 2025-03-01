using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Errors;
using Microsoft.AspNetCore.Identity;

namespace _1_BusinessLayer.Concrete.Tools.Mappers
{
    public static class ResultMapper
    {
        public static IdentityResult ToIdentityResult(this SignInResult signInResult)
        {
            var errors = new List<IdentityError>();

            if (signInResult.Succeeded)
            {
                return IdentityResult.Success;
            }

            if (signInResult.IsLockedOut)
            {
                errors.Add(new ForbiddenError("The user account is locked out."));

            }

            if (signInResult.IsNotAllowed)
            {
                errors.Add(new UnauthorizedError("The user is not allowed to sign in."));

            }

            if (signInResult.RequiresTwoFactor)
            {
                errors.Add(new ValidationError("Two-factor authentication is required."));

            }

            if (!signInResult.Succeeded && !signInResult.IsLockedOut && !signInResult.IsNotAllowed && !signInResult.RequiresTwoFactor)
            {
                errors.Add(new UnexpectedError("An unexpected error occurred during the sign-in process."));
            }

            return IdentityResult.Failed(errors.ToArray());
        }
    }
}
