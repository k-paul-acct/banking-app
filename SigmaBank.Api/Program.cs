using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SigmaBank.Api.Data;
using SigmaBank.Api.Data.Models;
using SigmaBank.Api.Extensions;
using SigmaBank.Api.Options;
using SigmaBank.Api.Services;

const string corsAllowLocalhostOriginPolicyName = "_corsAllowLocalhostOrigin";
const string siteCookie = "_siteDefault";
const string normalUser = "_normal";

var builder = WebApplication.CreateBuilder(args);

//
// Services configuration.
//
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsAllowLocalhostOriginPolicyName,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

// builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IPasswordHasher<Employee>, PasswordHasher<Employee>>();

builder.Services.AddDbContext<SigmaBankContext>(o => o.UseNpgsql(builder.Configuration["ConnectionStrings:SigmaBank"]));

builder.Services.AddScoped<UserAuthService>();
builder.Services.AddScoped<EmployeeAuthService>();

builder.AddValidation();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "Telegram API key to access endpoints for a Telegram Bot",
            Name = "x-api-key",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "ApiKeyScheme"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "ApiKey",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    });

var app = builder.Build();

//
// App configuration.
//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsAllowLocalhostOriginPolicyName);
app.UseAuthentication();
app.UseAuthorization();

//
// Mapping.
//
app.MapSiteUserEndpoints();

// app.MapGet("/", () => "Hello World!");

//
// Starting the app.
//
app.Run();

//
// Helper methods.
//
static IResult? Validate<TModel>(TModel model, IValidator<TModel> validator)
{
    var validationResult = validator.Validate(model);
    if (validationResult.IsValid) return null;

    var errors = validationResult.Errors.Select(x => new
    {
        Field = x.PropertyName, Message = x.ErrorMessage
    });

    return Results.BadRequest(new
    {
        Error = "badRequest", ErrorDescription = "One or more validation errors occurred", Errors = errors
    });
}