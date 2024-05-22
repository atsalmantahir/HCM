namespace HumanResourceManagement.Application.Common.Models;

public class Result
{
    public Result(bool succeeded, IList<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToList();
    }

    public bool Succeeded { get; init; }

    public IList<string> Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, new List<string>());
    }

    public static Result Failure(IList<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T> : Result // Generic Result class inheriting from non-generic Result
{
    public Result(bool succeeded, T data, IList<string> errors)
        : base(succeeded, errors)
    {
        Data = data;
    }

    public T Data { get; init; }

    public new static Result<T> Success(T data)
    {
        return new Result<T>(true, data, new List<string>());
    }

    public new static Result<T> Failure(IList<string> errors)
    {
        return new Result<T>(false, default, errors);
    }
}
