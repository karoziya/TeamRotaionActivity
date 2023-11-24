using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Database.Data.Configurations;

/// <summary>
/// Конфигурация базы для <see cref="ActivityWork"/>.
/// </summary>
public class ActivityWorkConfiguration : IEntityTypeConfiguration<ActivityWork>
{
    public void Configure(EntityTypeBuilder<ActivityWork> builder)
    {
        throw new NotImplementedException();
    }
}

