using System.Text.Json;
using RestEase;
using RestEase.Implementation;
using SkillSystem.Core.Models;

namespace SkillSystem.Client.Core;

public class Client
{
    private readonly IRequester requester;
    private readonly JsonSerializerOptions jsonSerializerOptions;

    public Client(IRequester requester)
    {
        this.requester = requester;
        jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    }

    public async Task<ClientResult<T>> SendRequestAsync<T>(RequestInfo requestInfo)
    {
        requestInfo.AllowAnyStatusCode = true;

        var response = await requester.RequestWithResponseAsync<T>(requestInfo);

        return response.ResponseMessage.IsSuccessStatusCode
            ? ClientResults.Success((int)response.ResponseMessage.StatusCode, response.GetContent())
            : ClientResults.Fail<T>(
                (int)response.ResponseMessage.StatusCode,
                response.StringContent is not null
                    ? JsonSerializer.Deserialize<ErrorResponse>(response.StringContent, jsonSerializerOptions)?.Error
                    : null);
    }

    public async Task<ClientResult> SendRequestAsync(RequestInfo requestInfo)
    {
        requestInfo.AllowAnyStatusCode = true;

        var response = await requester.RequestWithResponseMessageAsync(requestInfo);

        return response.IsSuccessStatusCode
            ? ClientResults.Success((int)response.StatusCode)
            : ClientResults.Fail(
                (int)response.StatusCode,
                JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync())?.Error);
    }
}
