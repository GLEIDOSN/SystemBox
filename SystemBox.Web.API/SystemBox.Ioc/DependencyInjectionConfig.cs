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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDadosPLCService, DadosPLCService>();
            services.AddScoped<EasyModbus.ModbusClient>(); // Registre ModbusClient como um serviço
        }
    }
}
