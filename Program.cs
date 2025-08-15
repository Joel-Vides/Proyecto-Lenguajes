using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Terminal.API.Services;
using Terminal.API.Services.Interfaces;
using Terminal.Database;
using Terminal.Extensions;
using Terminal.Filters;
using Terminal.Helpers;
using Terminal.Services;
using Terminal.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddDbContext<TerminalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Acceder Al contexto HTTP

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


//builder.Services.AddIdentity<UserEntity, RoleEntity>()
//    .AddEntityFrameworkStores<TerminalDbContext>()
//    .AddDefaultTokenProviders();



builder.Services.AddControllers();

builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IBusService, BusService>();
builder.Services.AddTransient<IHorarioService, HorarioService>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IRutaService, RutaService>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfig(builder.Configuration);


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
