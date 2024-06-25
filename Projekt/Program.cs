using Microsoft.EntityFrameworkCore;
using Projekt.Contracts;
using Projekt;
using Projekt.Helpers;
using Projekt.Models;
using Projekt.Persistance;
using Projekt.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Projekt.Middleware;
using FluentValidation;
using Projekt.Dto.Movie;
using Projekt.Dto.Validators;
using FluentValidation.AspNetCore;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projekt API v1"); c.RoutePrefix = string.Empty; }) ;
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
}