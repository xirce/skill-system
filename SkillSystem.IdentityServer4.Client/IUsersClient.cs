using Refit;
using SkillSystem.IdentityServer4.Client.Requests;
using SkillSystem.IdentityServer4.Client.Responses;

namespace SkillSystem.IdentityServer4.Client;

public interface IUsersClient
{
    [Get("/users/{userId}")]
    Task<User> GetUserById(Guid userId);

    [Post("/users")]
    Task<BatchGetUsersResponse> BatchGetUsers(BatchGetUsersRequest request);

    [Get("/users/search")]
    Task<SearchUsersResponse> SearchUsers([Query] SearchUsersRequest request);
}
