using FluentValidation;
using InsurtixTask.Application.Options;
using InsurtixTask.Application.RequestObjects;
using Microsoft.Extensions.Options;

namespace InsurtixTask.Application.Validators;

public class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator(IOptions<ValidationOptions> settings)
    {
        RuleFor(x => x.Isbn)
            .NotEmpty()
            .WithMessage("ISBN is required.")
            .Matches(settings.Value.IsbnRegex)
            .WithMessage("Must be valid ISBN format.");

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage("Author is required.")
            .Must(names => names.FirstOrDefault(n => n.Length < 2) == null)
            .WithMessage("Each author name must be at least 2 characters long.");

        RuleFor(x => x.Year)
            .NotEmpty()
            .WithMessage("Year is required.")
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("Year cannot be in the future.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be at least 0.");

        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Title is required")
            .Must(title => title.Value.Length >= 2)
            .WithMessage("title must be at least 2 characters long.");
    }
}
