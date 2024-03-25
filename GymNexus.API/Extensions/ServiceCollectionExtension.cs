using System.Security.Claims;
using GymNexus.Core.Contracts;
using GymNexus.Core.Services;
using GymNexus.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymNexus.Core.Utils;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMarketplaceService, MarketplaceService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<INomenclatureService, NomenclatureService>();
        services.AddScoped<IStoreService, StoreService>();

        return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("GymNexus")
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.User.RequireUniqueEmail = true;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration[JwtConfigs.Issuer],
                    ValidAudience = configuration[JwtConfigs.Audience],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[JwtConfigs.Key]))
                };
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration[FacebookAuthConfig.AppId];
                facebookOptions.AppSecret = configuration[FacebookAuthConfig.AppSecret];
                facebookOptions.Fields.Add("picture");
                facebookOptions.Events = new OAuthEvents()
                {
                    OnCreatingTicket = context =>
                    {
                        var identity = (ClaimsIdentity)context.Principal?.Identity!;
                        var picture = context.User.GetProperty("picture").GetProperty("data").GetProperty("url")
                            .ToString();
                        identity.AddClaim(new Claim("picture", picture));
                        return Task.CompletedTask;
                    }
                };
                facebookOptions.CallbackPath = new PathString("/login/facebook-callback");
            });

        return services;
    }

    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApi.Models.OpenApiInfo { Title = "GymNexus.API", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = OpenApi.Models.ParameterLocation.Header,
                Type = OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new OpenApi.Models.OpenApiReference
                        {
                            Type = OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }
}