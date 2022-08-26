using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.EntityFramework;
using Integral.EntityFramework.Services;
using Integral.RestApi.Middlewares;
using Integral.RestApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = ".Auth";
});
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddSingleton<IPasswordHashService, PasswordHashService>();

builder.Services.AddSingleton<IUserDataService, UserDataService>();
builder.Services.AddSingleton<IMeetingDataService, MeetingDataService>();
builder.Services.AddSingleton<IGroupDataService, GroupDataService>();
builder.Services.AddSingleton<IDataService<Student>, StudentDataService>();

builder.Services.AddSingleton<IDataManipulationService<User>, UserDataManipulationService>();
builder.Services.AddSingleton<IDataManipulationService<Meeting>, MeetingDataManipulationService>();
builder.Services.AddSingleton<IDataManipulationService<Group>, GroupDataManipulationService>();
builder.Services.AddSingleton<IDataManipulationService<Student>, StudentDataManipulationService>();

builder.Services.AddSingleton<IntegralDbContextFactory>();

builder.Services.AddSingleton<ILogger, FileLoggerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
