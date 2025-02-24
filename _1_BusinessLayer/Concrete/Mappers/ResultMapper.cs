using _1_BusinessLayer.Concrete.Errors;
using Microsoft.AspNetCore.Identity;

public static class ResultMapper
{
    public static IdentityResult ToIdentityResult(this SignInResult result, List<IdentityError>? extraErrors = null)
    {
        if (result.Succeeded)
        {
            return IdentityResult.Success;
            
        }

        var errors = new List<IdentityError>();

        if (result.IsLockedOut)
        {
            errors.Add(new UnauthorizedError("User account is locked due to multiple failed login attempts."));
        }

        if (result.RequiresTwoFactor)
        {
            errors.Add(new UnauthorizedError("Two-factor authentication is required for login."));
        }
        
        if (!result.Succeeded)
        {
            errors.Add(new UnauthorizedError("Invalid username or password."));
        }

        if (extraErrors != null)
        {
            errors.AddRange(extraErrors);
        }

        return IdentityResult.Failed(errors.ToArray()); 
    }
}
