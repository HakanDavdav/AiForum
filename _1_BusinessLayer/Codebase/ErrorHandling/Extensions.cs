using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public static class Extensions
{
    public static IdentityResult ToIdentityResult(this SignInResult signInResult)
    {
        if (signInResult == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = ErrorType.AuthenticationError.ToString(),
                Description = "SignInResult is null"
            });
        }

        if (signInResult.Succeeded)
            return IdentityResult.Success;

        var errors = new List<IdentityError>();

        // LockedOut → Forbidden (403) veya Custom ErrorType
        if (signInResult.IsLockedOut)
            errors.Add(new IdentityError
            {
                Code = ErrorType.Forbidden.ToString(),
                Description = "The user is locked out."
            });

        // NotAllowed → Unauthorized (401)
        if (signInResult.IsNotAllowed)
            errors.Add(new IdentityError
            {
                Code = ErrorType.Unauthorized.ToString(),
                Description = "The user is not allowed to sign in."
            });

        // RequiresTwoFactor → AuthenticationError (401) veya Custom
        if (signInResult.RequiresTwoFactor)
            errors.Add(new IdentityError
            {
                Code = ErrorType.AuthenticationError.ToString(),
                Description = "Two-factor authentication is required."
            });

        // Eğer başka bir sebep yoksa genel Failed → BadRequest (400)
        if (!signInResult.Succeeded && errors.Count == 0)
            errors.Add(new IdentityError
            {
                Code = ErrorType.BadRequest.ToString(),
                Description = "Sign-in failed for unknown reason."
            });

        return IdentityResult.Failed(errors.ToArray());
    }
}