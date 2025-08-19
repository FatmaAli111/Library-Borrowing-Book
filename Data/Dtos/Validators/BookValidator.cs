using Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.Validators
{
   public class BookValidator:AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            applyValidators(); 
        }

        public void applyValidators()
        {
            RuleFor(x => x.Title)
                        .NotEmpty().WithMessage("Title is required.")
                        .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(50).WithMessage("Author name must not exceed 50 characters.");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Length(13).WithMessage("ISBN must be exactly 13 characters.");
        }
    }
}
