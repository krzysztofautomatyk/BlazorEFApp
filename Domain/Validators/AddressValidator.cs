using BlazorEFApp.Domain.Entities;
using FluentValidation;

namespace BlazorEFApp.Domain.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required")
            .MaximumLength(100).WithMessage("Street cannot exceed 100 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(50).WithMessage("City cannot exceed 50 characters");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required")
            .MaximumLength(10).WithMessage("Postal code cannot exceed 10 characters")
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Invalid postal code format (XX-XXX)");

        RuleFor(x => x.ClientTypeId)
            .NotEmpty().WithMessage("Client type is required")
            .GreaterThan(0).WithMessage("Please select a valid client type");
    }
}
