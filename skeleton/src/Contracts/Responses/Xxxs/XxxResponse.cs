namespace IMOv2.Api.Contracts.Responses.Xxxs;

public class XxxResponse
{
    public Guid Id { get; init; }

    public string Username { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;
    public DateTime DateOfBirth { get; init; } = default!;
}
