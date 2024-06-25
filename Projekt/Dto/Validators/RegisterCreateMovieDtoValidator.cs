using FluentValidation;
using Projekt.Dto.Movie;
using Projekt.Services;

namespace Projekt.Dto.Validators
{
    public class RegisterCreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
    {
        private readonly IMovieService _movieService;
        public RegisterCreateMovieDtoValidator()
        {
            // reguły walidacyjne
            RuleFor(p => p.Title)
            .NotEmpty()
           .MinimumLength(2)
           .MaximumLength(20)
           .Must(BeUniqueName)
            .WithMessage("Nazwa filmu musi być unikalna.");
        }
        public bool BeUniqueName(string title)
        {
            return _movieService.IsInUse(title);
        }
    }
}
