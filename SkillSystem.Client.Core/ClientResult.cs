using SkillSystem.Core.Models;

namespace SkillSystem.Client.Core;

public class ClientResult
{
    public int StatusCode { get; }
    public Error? Error { get; }

    public bool IsSuccess => Error is null && StatusCode is >= 200 and <= 299;

    internal ClientResult(int statusCode, Error? error = null)
    {
        StatusCode = statusCode;
        Error = error;
    }

    public void EnsureSuccess()
    {
        if (!IsSuccess)
            throw new ClientResultException("Result is not successful");
    }
}
