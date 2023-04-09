using SkillSystem.Core.Models;

namespace SkillSystem.Client.Core;

public static class ClientResults
{
    public static ClientResult<T> Success<T>(int statusCode, T response)
    {
        return new ClientResult<T>(statusCode, response);
    }

    public static ClientResult<T> Fail<T>(int statusCode, Error? error)
    {
        return new ClientResult<T>(statusCode, error);
    }

    public static ClientResult Success(int statusCode)
    {
        return new ClientResult(statusCode);
    }

    public static ClientResult Fail(int statusCode, Error? error)
    {
        return new ClientResult(statusCode, error);
    }
}
