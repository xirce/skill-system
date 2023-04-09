using SkillSystem.Core.Models;

namespace SkillSystem.Client.Core;

public class ClientResult<TResponse> : ClientResult
{
    private readonly TResponse? response;

    public TResponse? Response
    {
        get
        {
            EnsureSuccess();
            return response;
        }
    }

    internal ClientResult(int statusCode, TResponse? response) : base(statusCode)
    {
        this.response = response;
    }

    internal ClientResult(int statusCode, Error? error) : base(statusCode, error)
    {
    }
}
