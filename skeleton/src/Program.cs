using FastEndpoints;
using FastEndpoints.Swagger;
using IMOv2.Api.Contracts.Responses;
using IMOv2.Api.Database;
using IMOv2.Api.Services;
using IMOv2.Api.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

config.SetBasePath(Directory.GetCurrentDirectory())
    .AddYamlFile("appsettings.yaml")
    .AddEnvironmentVariables()
    .Build();

var server = config.GetValue<string>("ImoV2Db:DbServer");
var port = config.GetValue<string>("ImoV2Db:DbPort");
var dbname = config.GetValue<string>("ImoV2Db:DbName");
var userId = config.GetValue<string>("ImoV2Db:DbUsername");
var password = config.GetValue<string>("ImoV2Db:DbPassword");

var connectionString = $"Server={server};Port={port};Database={dbname};User={userId};Password={password};SSL Mode=None;AllowPublicKeyRetrieval=True;default command timeout=0;";

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IRepository<,,>), typeof(GenericRepository<,,>));
builder.Services.AddScoped<IXxxService, XxxService>();
builder.Services.AddScoped<ImoV2DbContext>();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.Errors.ResponseBuilder = (failures, _, _) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Run();
