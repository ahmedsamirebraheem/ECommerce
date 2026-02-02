using ECommerce.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.CustomMiddleWare;


public class ExceptionHandlerMiddleWare(RequestDelegate next,ILogger<ExceptionHandlerMiddleWare> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
            await HandelNotFoundEndPointAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"Something Went wrong");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problem     = new ProblemDetails
            {
                Title = "Error while processing http request",
                Status = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                },
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = problem.Status.Value;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    private static async Task HandelNotFoundEndPointAsync(HttpContext context)
    {
        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            var problem = new ProblemDetails
            {
                Title = "Error while processing http request -EndPoint Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"EndPoint {context.Request.Path}",
                Instance = context.Request.Path
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
