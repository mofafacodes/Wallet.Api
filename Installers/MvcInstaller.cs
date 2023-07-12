using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Text;
using Wallet.Api.Options;

namespace Wallet.Api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configurtation)
        {
            //jwt authentication registrations
            var jwtSettings = new JwtSettings();
            configurtation.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            //mvc registration
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            //adds the bearer token configuration
            {
                x.SaveToken = true;
                //token validation parameters will be used valid tokens as request comes into the controllers
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true

                };
            });
          
            //configuring services for swgger to work
            //requires boilerplate configurations
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Wallet API", Version = "v1" });

                //updating to let swagger know baout authentication
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    //here we will define the type of authentication swagger needs to support
                    //all we need here is bearer token support
                    //JWt is a bearer token
                    {"Bearer", new string[0] }

                };

                x.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using theb bearer scheme",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                x.AddSecurityRequirement(security);

            });

        }

    }
}
