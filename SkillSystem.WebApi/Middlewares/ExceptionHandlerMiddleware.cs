using System.Net;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Core.Models;

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

        response.StatusCode = exception switch
        {
            EntityNotFoundException => (int)HttpStatusCode.NotFound,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        await response.WriteAsJsonAsync(
            new ErrorResponse(response.StatusCode, new Error { Code = "Code", Message = exception.Message }));
    }
}
