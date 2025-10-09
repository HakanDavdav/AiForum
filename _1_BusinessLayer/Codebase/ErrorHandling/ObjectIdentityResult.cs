using Microsoft.AspNetCore.Identity;

public class ObjectIdentityResult<T> : IdentityResult
{
    public T? Data { get; set; }
    public IdentityError[] ObjectIdentityErrors { get; set; } = Array.Empty<IdentityError>();

    public new IEnumerable<IdentityError> Errors => ObjectIdentityErrors.AsEnumerable();

    public static ObjectIdentityResult<T> Succeeded(T data)
    {
        var result = new ObjectIdentityResult<T>
        {
            Data = data
        };
        // Set Succeeded property using base class setter
        result.GetType().BaseType?.GetProperty("Succeeded")?.SetValue(result, true);
        return result;
    }

    public static ObjectIdentityResult<T> Failed(params IdentityError[] errors)
    {
        var result = new ObjectIdentityResult<T>
        {
            ObjectIdentityErrors = errors
        };
        // Set Succeeded property using base class setter
        result.GetType().BaseType?.GetProperty("Succeeded")?.SetValue(result, false);
        return result;
    }
}
