namespace IMOv2.Api.Contracts.Entities;

public class XxxEntity : AuditableBaseEntity<Guid>
{
    public string Username { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;
    public DateTime DateOfBirth { get; init; } = default!;
}
