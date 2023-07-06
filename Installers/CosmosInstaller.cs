using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Api.Domain;

namespace Wallet.Api.Installers
{
    public class CosmosInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configurtation)
        {
            var cosmosStoreSettings = new CosmosStoreSettings(
                configurtation["CosmosStoreSettings:DatabaseName"],
                configurtation["CosmosStoreSettings:AccountUri"],
                configurtation["CosmosStoreSettings:AccountKey"],
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp });

            services.AddCosmosStore<CosmosAccountDto>(cosmosStoreSettings);
        }
    }
}
 