using Core.Results;
using Core.Results.Application.Common;
using FluentValidation;
using FluentValidation.Results;

public class ValidationService
{
    public static async Task<IEnumerable<string>> Validate<T>(T model, IValidator<T> validator)
    {
        ValidationResult result = validator.Validate(model);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
            return errorMessages;  
        }
        return null;
    }
}
