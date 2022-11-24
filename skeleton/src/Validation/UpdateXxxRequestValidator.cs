using FluentValidation;
using IMOv2.Api.Contracts.Requests.Xxxs;

namespace IMOv2.Api.Validation;

public class UpdateXxxRequestValidator : AbstractValidator<UpdateXxxRequest>
{
    public UpdateXxxRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.EmailAddress).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}
