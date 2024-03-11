using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Voam.Core.Contracts;
using Voam.Core.Services;
using Voam.Infrastructure.Data;
using Voam.Infrastucture.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("https://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<VoamDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //options.Password - add reqs
            }).AddEntityFrameworkStores<VoamDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateActor = true,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     RequireExpirationTime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = config.GetSection("Jwt:Issuer").Value,
                     ValidAudience = config.GetSection("Jwt:Audience").Value,

                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value))
                 };
            });

            return services;
        }
    }
}
