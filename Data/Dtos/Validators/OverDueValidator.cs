using Data.Dtos;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class OverdueBookValidator : AbstractValidator<OverdueBookDto>
    {
        public OverdueBookValidator()
        {
            RuleFor(x => x.BookTitle)
                .NotEmpty().WithMessage("Book title is required.")
                .MaximumLength(100).WithMessage("Book title must be less than 100 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(100).WithMessage("User name must be less than 100 characters.");

            RuleFor(x => x.DueDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Due date cannot be in the future.");

            RuleFor(x => x.DaysOverdue)
                .GreaterThanOrEqualTo(0).WithMessage("Days overdue cannot be negative.");
        }
    }
}
