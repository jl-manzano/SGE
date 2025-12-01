using Data.Repositories;
using Domain.Interfaces;
using Domain.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionRoot
{
    /// <summary>
    /// Configura la inyección de dependencias de la aplicación.
    /// Registra los servicios necesarios en el contenedor de dependencias.
    /// </summary>
    public static class InversifyConf
    {
        /// <summary>
        /// Configura la inyección de dependencias para los servicios de Persona y Departamento.
        /// </summary>
        /// <param name="services">Colección de servicios para la inyección de dependencias.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <returns>La colección de servicios con los servicios registrados.</returns>
        public static IServiceCollection AddCompositionRoot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();

            services.AddScoped<IPersonaUseCases, PersonaUseCases>();
            services.AddScoped<IDepartamentoUseCases, DepartamentoUseCases>();

            return services;
        }
    }
}
