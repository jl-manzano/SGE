using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data.Repositories;
using Domain.Interfaces;
using Domain.UseCases;

namespace CompositionRoot
{
    /// <summary>
    /// Configuración del Composition Root para registrar dependencias (repositorios y casos de uso)
    /// en el contenedor de inyección de dependencias.
    /// </summary>
    public static class InversifyConfig
    {
        /// <summary>
        /// Registra las dependencias de la aplicación en el contenedor de DI:
        /// repositorios (capa de datos) y casos de uso (capa de dominio/aplicación).
        /// </summary>
        /// <param name="services">Colección de servicios donde se registran las dependencias.</param>
        /// <param name="configuration">Configuración de la aplicación (por si se requiere para futuros registros).</param>
        /// <returns>La misma colección de servicios para permitir encadenamiento.</returns>
        public static IServiceCollection AddCompositionRoot(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();

            // UseCases
            services.AddScoped<IDepartamentoUseCases, DepartamentoUseCases>();
            services.AddScoped<IPersonaUseCases, PersonaUseCases>();

            return services;
        }
    }
}
