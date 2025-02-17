public class Result 
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public Result(bool succeeded, string message = null, IEnumerable<string> errors = null)
    {
        Succeeded = succeeded;
        Message = message;
        Errors = errors ?? new List<string>();
    }

    public static Result Success(string message = null) => new Result(true, message);

    public static Result Failure(string message, IEnumerable<string> errors) => new Result(false, message, errors);
}
