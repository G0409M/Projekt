using Microsoft.EntityFrameworkCore;
using Projekt.Domain.Contracts;
using Projekt;
using Projekt.Domain.Helpers;
using Projekt.Domain.Models;
using Projekt.Application.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using FluentValidation;
using Projekt.SharedKernel.Dto.Movie;
using Projekt.Application.Validators;
using FluentValidation.AspNetCore;
using Projekt.Infrastructure;
using Projekt.Infrastructure.Repositories;
using Projekt.Web.API.Middleware;
using Projekt.Application.Mappings;
using NLog.Web;
using NLog;

class Program
{
    static void Main(string[] args)
    {
        // Add services to the container.
        var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug("init main");
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projekt API", Version = "v1" });
            });

            // Rejestracja kontekstu bazy w kontenerze IoC
            var sqliteConnectionString = "Data Source=Projekt.WebAPI.db";
            builder.Services.AddDbContext<ProjektDbContext>(options =>
                options.UseSqlite(sqliteConnectionString));

            builder.Services.AddScoped<IProjektUnitOfWork, ProjektUnitOfWork>();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieRoleRepository, MovieRoleRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IMovieRoleService, MovieRoleService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ExceptionMiddleware>();

            builder.Services.AddScoped<DataSeeder>();

            builder.Services.AddScoped<IValidator<CreateMovieDto>, RegisterCreateMovieDtoValidator>();

            // Rejestracja AutoMapper
            builder.Services.AddAutoMapper(typeof(ProjektMapper));

            // Rejestracja automatycznej walidacji
            // FluentValidation waliduje i przekazuje wynik przez ModelState
            builder.Services.AddFluentValidationAutoValidation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projekt API v1"); });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionMiddleware>();

            //...
            // uruchomienie seedera
            using (var scope = app.Services.CreateScope())
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                dataSeeder.Seed();
            }
            app.Run();

        }
        catch (Exception exception)
        {
            // NLog: catch setup errors
            logger.Error(exception, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }
    }
}