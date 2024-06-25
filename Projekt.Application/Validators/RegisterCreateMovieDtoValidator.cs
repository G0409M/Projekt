using FluentValidation;
using Projekt.SharedKernel.Dto.Movie;
using Projekt.Application.Services;

namespace Projekt.Application.Validators
{
    public class RegisterCreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
    {
        private readonly Func<IMovieService> _movieServiceFactory;

        public RegisterCreateMovieDtoValidator(Func<IMovieService> movieServiceFactory)
        {
            _movieServiceFactory = movieServiceFactory;

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
            var movieService = _movieServiceFactory();
            return !movieService.IsInUse(title);
        }
    }


}
