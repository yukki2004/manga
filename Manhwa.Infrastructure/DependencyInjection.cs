using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using Manhwa.Infrastructure.FileStorage;
using Manhwa.Infrastructure.Identity;
using Manhwa.Infrastructure.Persistence;
using Manhwa.Infrastructure.Persistence.Repositories;
using Manhwa.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)
                       .UseSnakeCaseNamingConvention());
            // jwt
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["accessToken"];
                        return Task.CompletedTask;
                    }
                };
            });
            services.Configure<FileStorageOptions>(configuration.GetSection("FileStorage"));    
            // service
            services.AddScoped<IStorageService, LocalStorageService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, JwtService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IStoryGenreRepository, StoryGenreRepository>();
            services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<IChapterImageRepository, ChapterImageRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IVolumeImageRepository, VolumeImageRepository>();
            services.AddScoped<IVolumeRepository, VolumeRepository>();
            return services;
        }
    }
}
