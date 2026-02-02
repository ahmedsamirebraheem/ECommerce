using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.CustomMiddleWare;


public class ExceptionHandlerMiddleWare(RequestDelegate next,ILogger<ExceptionHandlerMiddleWare> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
            if(context.Response.StatusCode == StatusCodes.Status404NotFound)
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
        catch (Exception ex)
        {
            logger.LogError("Something Went wrong");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problem     = new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = context.Request.Path
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
