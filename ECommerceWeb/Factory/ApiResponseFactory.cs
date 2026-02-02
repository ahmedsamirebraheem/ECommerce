using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Factory;

public static class ApiResponseFactory
{
    public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
    {
        var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors
                    .Select(e => e.ErrorMessage))
                    .ToArray();

        var problemDetails = new ProblemDetails()
        {
            Title = "validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "One or more validation error.",
            Extensions =
                    {
                        { "errors", errors }
                    }
        };
        return new BadRequestObjectResult(problemDetails);
    }
}
