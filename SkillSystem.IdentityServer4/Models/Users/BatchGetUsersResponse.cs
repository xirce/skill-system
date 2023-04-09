namespace SkillSystem.IdentityServer4.Models.Users;

public record BatchGetUsersResponse(IReadOnlyCollection<UserModel> Users);
