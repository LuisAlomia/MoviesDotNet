using Application.UseCases;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Persistences.EntityFramework.ContextDB;
using Infrastructure.Persistences.EntityFramework.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB context
builder.Services.AddDbContext<ContextDatabase>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ContextDatabase"));
});

//Inyection dependencies
builder.Services.AddScoped<IMovieRepository, MovieRepoImpl>();
builder.Services.AddScoped<IMovieServices, MoviesUseCase>();

builder.Services.AddScoped<IUserRepository, UserRepoImpl>();
builder.Services.AddScoped<IUserServices, UsersUseCase>();

builder.Services.AddScoped<IAuthUserServices, AuthUseCase>();

builder.Services.AddAutoMapper(typeof(MappersAdmind));

//Autentication Bearer Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    };
});

//Authorization Politic

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RoleAdmin", policy => policy.RequireClaim("Role", "admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
