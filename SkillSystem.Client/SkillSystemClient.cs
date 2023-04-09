using RestEase.Implementation;
using SkillSystem.Client.Core;

namespace SkillSystem.Client;

public class SkillSystemClient : ISkillSystemClient
{
    private readonly Core.Client client;

    public SkillSystemClient(Core.Client client)
    {
        this.client = client;
    }

    public async Task<ClientResult<Role>> GetRole(int roleId)
    {
        return await client.SendRequestAsync<Role>(new RequestInfo(HttpMethod.Get, $"roles/{roleId}"));
    }
}
