using SkillSystem.Client.Core;

namespace SkillSystem.Client;

public interface ISkillSystemClient
{
    Task<ClientResult<Role>> GetRole(int roleId);
}

public class Role
{
    public int Id { get; set; }
    public string Title { get; set; }
}
