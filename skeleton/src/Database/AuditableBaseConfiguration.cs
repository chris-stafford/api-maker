using IMOv2.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMOv2.Api.Database;

public abstract class AuditableBaseConfiguration<T, TKey> : IEntityTypeConfiguration<T>
    where T : AuditableBaseEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        var defaultBy = "System";
        builder.Property(p => p.Created).ValueGeneratedOnAdd();
        builder.Property(p => p.CreatedBy).ValueGeneratedOnAdd().HasDefaultValue(defaultBy);
        builder.Property(p => p.Modified).ValueGeneratedOnUpdate();
        builder.Property(p => p.ModifiedBy).ValueGeneratedOnUpdate().HasDefaultValue(defaultBy);
    }
}