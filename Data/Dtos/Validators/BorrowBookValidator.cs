using Data.Dtos;
using FluentValidation;

namespace Infrastructure.Validators
{
    public class BorrowBookValidator : AbstractValidator<BorrowBookDto>
    {
        public BorrowBookValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.BookCopyId)
                .GreaterThan(0).WithMessage("BookCopyId must be greater than 0.");
        }
    }
}
