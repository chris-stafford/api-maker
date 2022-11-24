using IMOv2.Api.Domain.Common;
using IMOv2.Api.Domain.Ids;

namespace IMOv2.Api.Domain;

public class Xxx
{
    public XxxId Id { get; init; } = XxxId.From(Guid.NewGuid());

    public Username Username { get; init; } = default!;
    public FullName FullName { get; init; } = default!;
    public EmailAddress EmailAddress { get; init; } = default!;
    public DateOfBirth DateOfBirth { get; init; } = default!;
}
