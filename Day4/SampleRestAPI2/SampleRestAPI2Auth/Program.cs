using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4;
using SampleRestAPI2Auth.DAL.Repository;
using SampleRestAPI2Auth.BLL.Services;
using SampleRestAPI2Auth.BLL.Interfaces;
using SampleRestAPI2Auth.External;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

X509Certificate2 cert = new X509Certificate2("my-app-auth.pfx", builder.Configuration["Certificate:Password"]);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SampleRestAPI2AuthContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddIdentityServer(options =>
{
    options.Authentication.CookieAuthenticationScheme = "none";
    options.IssuerUri = builder.Configuration["AuthorizationServer:Address"];
})
            .AddSigningCredential(cert)
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidatorService>()
            .AddProfileService<UserProfile>()
            .AddProfileService<UserProfile>()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = opt =>
        opt.UseSqlServer(
            builder.Configuration["ConnectionStrings:DefaultConnection"],
                        sql => sql.MigrationsAssembly("SampleRestAPI2Auth.DAL"));
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = opt =>
        opt.UseSqlServer(
            builder.Configuration["ConnectionStrings:DefaultConnection"],
                        sql => sql.MigrationsAssembly("SampleRestAPI2Auth.DAL"));
    options.EnableTokenCleanup = true;
    options.TokenCleanupInterval = 3600;
})
.AddPersistedGrantStore<PersistedGrantStore>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["AuthorizationServer:Address"];
    options.Audience = builder.Configuration["Service:Name"];
    options.RequireHttpsMetadata = false;
})
.AddCookie("none")
.AddGoogle("Google", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    options.SaveTokens = true;
    options.ClientId = "679400509968-dusc9gib5lf95il2veg2doaqh0joc3v4.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserAuthorization, UserAuthorization>();
builder.Services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();
builder.Services.AddSingleton<GoogleApi>();
builder.Services.AddScoped<IdentityServerApi>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Auth Service", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        },
                        new List<string>()
                        }
                });
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
app.UseIdentityServer();

app.MapControllers();

app.Run();
