using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamRotationActivity.Domain.Models;

namespace TeamRotationActivity.Database.Data.Configurations;

/// <summary>
/// Конфигурация базы для <see cref="Member"/>.
/// </summary>
public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        throw new NotImplementedException();
    }
}

