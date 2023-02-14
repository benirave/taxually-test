using System.Net;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
		try
		{
            await _next(context);
        }
        catch (Exception ex)
		{
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorResult
            {
                ErrorMessage= ex.Message
            });
		}
    }
}
