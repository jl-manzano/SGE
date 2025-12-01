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
    public static class InversifyConf
    {
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
