using BlazorEFApp.Domain.Entities;
using FluentValidation;

namespace BlazorEFApp.Domain.Validators;

public class ClientTypeValidator : AbstractValidator<ClientType>
{
    public ClientTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");
    }
}
