using ECommerce.Shared.Common_Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ApiBaseController : ControllerBase
{
    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();
        else
        {
            return HandleProblem(result.Errors);
        }
    }
    protected ActionResult<T> HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);
        else
        {
            return HandleProblem(result.Errors);
        }
    }
    private ActionResult HandleProblem(IReadOnlyList<Error> errors)
    {
        if(errors.Count == 0)
        {
            return Problem(
                title: "Unknown Error",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return HandleValidationProblem(errors);
        }
        return HandleSingleErrorProblem(errors[0]);
        
        
    }
    private ActionResult HandleSingleErrorProblem(Error error)
    {
        return Problem(
            title: error.Code,
            detail: error.Description,
            type: error.Type.ToString(),
            statusCode: error.Type switch
            {
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            }
        );
    }
    private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
    {
        var modelState = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(modelState);
    }
}
