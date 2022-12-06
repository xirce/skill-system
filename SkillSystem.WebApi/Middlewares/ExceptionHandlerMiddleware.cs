using System.Net;
using SkillSystem.Application.Common.Exceptions;

namespace SkillSystem.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;

        switch (exception)
        {
            case EntityNotFoundException notFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(notFoundException.Message);
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsJsonAsync(exception.Message);
                break;
        }
    }
}