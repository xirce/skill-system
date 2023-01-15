using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillSystem.IdentityServer4.Data.Entities;

namespace SkillSystem.IdentityServer4.Data;

public class SkillSystemIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public SkillSystemIdentityDbContext(DbContextOptions<SkillSystemIdentityDbContext> options) : base(options)
    {
    }
}