using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.Common_Result;

public class Result
{
    protected readonly List<Error> _errors = [];
    public bool IsSuccess => _errors.Count == 0;
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors => _errors;
    protected Result()
    {
        
    }
    protected Result(Error error)
    {
        _errors.Add(error);
    }
    protected Result(List<Error> errors)
    {
        _errors.AddRange(errors);
    }
    public static Result Ok()
    {
        return new Result();
    }
    public static Result Fail(Error error)
    {
        return new Result(error);
    }
    public static Result Fail(List<Error> errors)
    {
        return new Result(errors);
    }

}

public class Result<T> : Result
{
    private readonly T? _value;
    public T Value => IsSuccess? _value! : throw new InvalidOperationException("Cannot access the value");
    protected Result(T value)
    {
        _value = value;
    }
    private Result(Error error) : base(error)
    {
        _value = default;
    }
    private Result(List<Error> errors) : base(errors)
    {
        _value = default;
    }
    public static Result<T> Ok(T value) => new (value);
    
    public new static Result<T> Fail(Error error) => new (error);
    
    public new static Result<T> Fail(List<Error> errors) => new (errors);
    public static implicit operator Result<T>(T value) => Ok(value);
    public static implicit operator Result<T>(Error error) => Fail(error);
    public static implicit operator Result<T>(List<Error> errors) => Fail(errors);


}
