namespace UrlShortener.Application.Models;

public class Result
{
    public bool IsSuccess { get; }
    
    public IEnumerable<Error>? Errors { get; }

    protected Result(bool isSuccess, IEnumerable<Error>? errors)
    {
        if (isSuccess && errors is not null
            || !isSuccess && errors is null)
        {
            throw new ArgumentException("Inconsistent arguments: either success or errors must be specified");
        }
        
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }
    
    public static Result Failure(Error error)
    {
        return new Result(false, Enumerable.Repeat(error, 1));
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false, errors);
    }

    public static implicit operator Result(Error error)
    {
        return Result.Failure(error);
    }
}

public class Result<TData> : Result
{
    public TData? Data { get; }

    protected Result(bool isSuccess, TData? data, IEnumerable<Error>? errors) : base(isSuccess, errors)
    {
        Data = data;
    }

    public static Result<TData?> Success(TData? data)
    {
        return new Result<TData?>(true, data, null);
    }
    
    public new static Result<TData?> Failure(Error error)
    {
        return new Result<TData?>(false, default, Enumerable.Repeat(error, 1));
    }
    
    public new static Result<TData?> Failure(IEnumerable<Error> errors)
    {
        return new Result<TData?>(false, default, errors);
    }

    public static implicit operator Result<TData?>(Error error)
    {
        return Result<TData?>.Failure(error);
    }
}