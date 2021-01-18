using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Api.Options;
using TweetBook.Core.Business.Services;

namespace TweetBook.Api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {

            var settings = new JwtSettings();

            Configuration.GetSection(nameof(JwtSettings)).Bind(settings);

            Configuration.Bind(nameof(settings), settings);
            services.AddSingleton(settings);

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(x => 
                { 
                    x.SaveToken = true;
                    x.TokenValidationParameters =
                    new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                }
            ) ;
    

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Tweetbook API", Version = "v1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(
                    new OpenApiSecurityRequirement() {
                       {
                             new OpenApiSecurityScheme()
                             {
                                   Reference = new OpenApiReference
                                   {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                   }
                              },
                              new string[] { }
                        }
                  });
            });
        }
    }
}
