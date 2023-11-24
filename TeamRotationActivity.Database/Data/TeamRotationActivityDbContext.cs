using Microsoft.EntityFrameworkCore;
using TeamRotationActivity.Database.Data.Configurations;

namespace TeamRotationActivity.Database.Data;

/// <summary>
/// Контекст доступа для EFCore.
/// </summary>
public class TeamRotationActivityDbContext : DbContext
{
    public TeamRotationActivityDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

