using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.Common_Result;

public class Error
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ErrorType Type { get; set; }
    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }
    public static Error Failure(string code = "Generel.Failure", string description = "Generel.Failure has occure")
    {
        return new Error(code, description, ErrorType.Failure);
    }
    public static Error Validation(string code = "Generel.Validation", string description = "Generel.Validation has occure")
    {
        return new Error(code, description, ErrorType.Validation);
    }
    public static Error NotFound(string code = "Generel.NotFound", string description = "Generel.NotFound has occure")
    {
        return new Error(code, description, ErrorType.NotFound);
    }
    public static Error Unauthorized(string code = "Generel.Unauthorized", string description = "Generel.Unauthorized has occure")
    {
        return new Error(code, description, ErrorType.Unauthorized);
    }
    public static Error Forbidden(string code = "Generel.Forbidden", string description = "Generel.Forbidden has occure")
    {
        return new Error(code, description, ErrorType.Forbidden);
    }
    public static Error InvalidCredentials(string code = "Generel.InvalidCredentials", string description = "Generel.InvalidCredentials has occure")
    {
        return new Error(code, description, ErrorType.InvalidCredentials);
    }
}
