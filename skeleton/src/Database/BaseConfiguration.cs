using IMOv2.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMOv2.Api.Database;
public abstract class BaseConfiguration<T, TKey> : IEntityTypeConfiguration<T>
    where T : BaseEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
    }
}