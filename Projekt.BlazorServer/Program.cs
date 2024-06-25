using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Projekt.Application.Mappings;
using Projekt.Application.Services;
using Projekt.Application.Validators;
using Projekt.BlazorServer.Data;
using Projekt.Domain.Contracts;
using Projekt.SharedKernel.Dto.Movie;
using Microsoft.EntityFrameworkCore;
using Projekt.Infrastructure;
using Projekt.Infrastructure.Repositories;
using Projekt.SharedKernel.Dto;
using NLog.Web;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// Rejestracja AutoMapper
builder.Services.AddAutoMapper(typeof(ProjektMapper));
// FluentValidation waliduje i przekazuje wynik przez ModelState
builder.Services.AddFluentValidationAutoValidation();
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

//builder.Services.AddScoped<ExceptionMiddleware>();

builder.Services.AddScoped<DataSeeder>();

builder.Services.AddScoped<IValidator<CreateMovieDto>, RegisterCreateMovieDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
