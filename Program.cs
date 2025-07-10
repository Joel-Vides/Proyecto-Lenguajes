using Microsoft.EntityFrameworkCore;
using Terminal.Database;
using Terminal.Helpers;
using Scalar.AspNetCore;
using Terminal.Services.Interfaces;
using Terminal.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

 //Add services to the container.


builder.Services.AddDbContext<TerminalDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));




builder.Services.AddControllers();



builder.Services.AddTransient<IEmpresaService, EmpresaService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
