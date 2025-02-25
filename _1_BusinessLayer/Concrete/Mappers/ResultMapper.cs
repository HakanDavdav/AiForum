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

   
    public static IdentityResult ToIdentityResult(this IdentityResult result, List<IdentityError>? extraErrors = null)
    {
        if (result.Succeeded)
        {
            return IdentityResult.Success;
        }

        var errors = result.Errors.Select(error =>
        {
            IdentityError identityError = error.Code switch
            {
                "DuplicateUserName" => new ConflictError("This username is already taken.") { Code = error.Code },
                "DuplicateEmail" => new ConflictError("This email is already in use.") { Code = error.Code },
                "PasswordTooShort" => new ValidationError("Password must be at least 8 characters long.") { Code = error.Code },
                "PasswordRequiresUpper" => new ValidationError("Password must contain at least one uppercase letter.") { Code = error.Code },
                "PasswordRequiresLower" => new ValidationError("Password must contain at least one lowercase letter.") { Code = error.Code },
                "PasswordRequiresDigit" => new ValidationError("Password must contain at least one digit.") { Code = error.Code },
                "UserLockedOut" => new ForbiddenError("User account is locked due to multiple failed login attempts.") { Code = error.Code },
                "InvalidToken" => new UnauthorizedError("Invalid verification token.") { Code = error.Code },
                "UserNotConfirmed" => new ForbiddenError("User email is not confirmed.") { Code = error.Code },
                "InvalidEmail" => new ValidationError("Email is invalid") {Code = error.Code },
                _ => new InternalServerError($"Unexpected error: {error.Description}") { Code = error.Code }
            };

            return identityError;
        }).ToList(); 

        if (extraErrors != null)
        {
            errors.AddRange(extraErrors);
        }

        return IdentityResult.Failed(errors.ToArray());
    }

}
