using IMOv2.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMOv2.Api.Database;

public class XxxConfiguration : AuditableBaseConfiguration<XxxEntity, Guid>
{
    public override void Configure(EntityTypeBuilder<XxxEntity> builder)
    {
        base.Configure(builder);
    }
}