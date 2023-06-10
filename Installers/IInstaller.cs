using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wallet.Api.Installers
{
    public interface IInstaller
    {
        void InstallServices (IServiceCollection services, IConfiguration configurtation);
    }
}
