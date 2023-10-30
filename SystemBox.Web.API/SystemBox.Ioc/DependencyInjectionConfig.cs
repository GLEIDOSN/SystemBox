using Microsoft.Extensions.DependencyInjection;
using SystemBox.Data.Repositories;
using SystemBox.Service.Services;

namespace SystemBox.Ioc
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureServicesIoc(IServiceCollection services)
        {
            // Repositoriesserve
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Services
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
