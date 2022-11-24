using FluentValidation;
using IMOv2.Api.Contracts.Requests.Xxxs;

namespace IMOv2.Api.Validation;

public class CreateXxxRequestValidator : AbstractValidator<CreateXxxRequest>
{
    public CreateXxxRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}
