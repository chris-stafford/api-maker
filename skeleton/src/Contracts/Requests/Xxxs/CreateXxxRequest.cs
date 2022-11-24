namespace IMOv2.Api.Contracts.Requests.Xxxs;

public class CreateXxxRequest
{
    public string Username { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string EmailAddress { get; init; } = default!;
    public DateTime DateOfBirth { get; init; } = default!;
}
