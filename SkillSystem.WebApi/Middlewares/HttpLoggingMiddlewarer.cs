using System.Diagnostics;
using Serilog;

namespace SkillSystem.WebApi.Middlewares;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate next;

    public HttpLoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        LogRequest(context.Request);

        await next(context);

        LogResponse(context.Response, stopwatch.Elapsed);
    }

    private void LogRequest(HttpRequest request)
    {
        Log.Information("Received request {Method} {Path}{Query}", request.Method, request.Path, request.QueryString);
    }

    private void LogResponse(HttpResponse response, TimeSpan elapsed)
    {
        Log.Information("Response code = {ResponseCode}. Elapsed time = {ElapsedTime}.", response.StatusCode, elapsed);
    }
}
