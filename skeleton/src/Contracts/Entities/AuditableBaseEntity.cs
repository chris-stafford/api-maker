namespace IMOv2.Api.Contracts.Entities;

public abstract class AuditableBaseEntity<T> : BaseEntity<T>
{
    public bool IsDeleted { get; init; } = default;
    public DateTime? Created { get; init; } = default;
    public string CreatedBy { get; init; } = default!;
    public DateTime? Modified { get; init; } = default;
    public string ModifiedBy { get; init; } = default!;
}