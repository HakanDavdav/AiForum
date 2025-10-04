using Microsoft.AspNetCore.Identity;

public class AppObjectResult<T> : IdentityResult
{
    public T? Data { get; set; }
    public IdentityError[] ObjectIdentityErrors { get; set; } = Array.Empty<IdentityError>();

    // Base class Errors'ı gizle
    public new IEnumerable<IdentityError> Errors => ObjectIdentityErrors.AsEnumerable();

    public static AppObjectResult<T> Succeeded(T data)
    {
        return new AppObjectResult<T>
        {
            Succeeded = true,
            Data = data
        };
    }

    public static AppObjectResult<T> Failed(params IdentityError[] errors)
    {
        return new AppObjectResult<T>
        {
            Succeeded = false,
            ObjectIdentityErrors = errors
        };
    }
}
