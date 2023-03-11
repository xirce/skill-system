using Microsoft.AspNetCore.Identity;

namespace SkillSystem.IdentityServer4.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string FullName { get; }
}
