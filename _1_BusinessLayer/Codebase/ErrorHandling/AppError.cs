using Microsoft.AspNetCore.Identity;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

public enum ErrorType
{   
    ValidationError,      // 400
    Unauthorized,         // 401
    Forbidden,            // 403
    NotFound,             // 404
    Conflict,             // 409
    BadRequest,           // 400
    AuthenticationError,  // 401
    RateLimitExceeded,    // 429
    NotImplemented        // 501
}

public class AppError : IdentityError
{
    public AppError(ErrorType code, string description)
    {
        Code = code.ToString();
        Description = description;
    }
}

