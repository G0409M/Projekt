using Projekt.Domain.Exceptions;
using Projekt.SharedKernel.Dto.Movie;

namespace Projekt.Web.API.Middleware
{
    // Middleware do obsługi wybranych wyjątków
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<Exception> _logger;

        public ExceptionMiddleware(ILogger<Exception> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                _logger.LogDebug("Błąd 404 - nie znaleiono");
                await context.Response.WriteAsync(ex.Message);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogDebug("Błąd 400 - niepoprawne zapytanie");
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogDebug("Błąd 500 - wewnętrzny błąd serwera");
                await context.Response.WriteAsync("Something went wrong");
            }

        }
    }
}
