using FluentValidation;
using Data.Dtos;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Username can only contain letters and digits");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required");

        RuleFor(x => x.MembershipType)
            .NotEmpty().WithMessage("Membership type is required")
            .Must(type => new[] { "Standard", "Vip", "Premium" }.Contains(type))
            .WithMessage("Membership type must be one of: Standard, Vip, Premium");

        RuleFor(x => x.MembershipStatus)
            .NotEmpty().WithMessage("Membership status is required")
            .Must(status => new[] { "Available", "Suspended", "Expired" }.Contains(status))
            .WithMessage("Membership status must be one of: Available, Suspended, Expired");
    }
}
