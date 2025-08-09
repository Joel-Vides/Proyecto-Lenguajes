using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Terminal.Database;
using Terminal.Extensions;
using Terminal.Helpers;
using Terminal.Services;
using Terminal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

 //Add services to the container.


builder.Services.AddDbContext<TerminalDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));




builder.Services.AddControllers();



builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IBusService, BusService>();
builder.Services.AddTransient<IHorarioService, HorarioService>();

builder.Services.AddCorsConfiguration(builder.Configuration);


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
