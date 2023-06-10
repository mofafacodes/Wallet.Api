using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Wallet.Api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configurtation)
        {
            //mvc registration
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //configuring serces for swgger to work
            //requires boilerplate configurations
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Wallet API", Version = "v1" });
            });

        }

    }
}
