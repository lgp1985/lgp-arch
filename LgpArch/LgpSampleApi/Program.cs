using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{builder.Configuration["AzureAD:TenantId"]}/oauth2/authorize"),
                Scopes = { { builder.Configuration.GetSection("AzureAd")["Scopes"], "Access the API on your behalf" } }
            }
        },
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme } }, Array.Empty<string>()
        }
    });
});

builder.Services
    .AddDbContext<LgpArch.Database.LgpArchDbContext>()
    .Configure<LgpArch.Database.LgpArchDbContextOptions>(builder.Configuration.GetSection(LgpArch.Database.LgpArchDbContextOptions.Section))
    .AddHealthChecks()
    .AddDbContextCheck<LgpArch.Database.LgpArchDbContext>();

builder.Services.AddScoped<LgpArch.Facades.Repositories.IWeatherRepository, LgpArch.Database.Repositories.WeatherRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
            c.OAuthClientId(builder.Configuration["Swagger:ClientId"]);
            c.OAuthRealm(builder.Configuration["AzureAD:ClientId"]);
            c.OAuthScopeSeparator(" ");
            c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "resource", builder.Configuration["AzureAD:ClientId"] } });
        });

    app.Services.CreateScope().ServiceProvider
        .GetRequiredService<LgpArch.Database.LgpArchDbContext>()
        .Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
