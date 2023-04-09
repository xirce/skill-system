namespace SkillSystem.Core.Models;

public class ErrorResponse
{
    public int StatusCode { get; }
    public Error Error { get; }

    public ErrorResponse(int statusCode, Error error)
    {
        StatusCode = statusCode;
        Error = error;
    }
}
