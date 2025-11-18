using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UseCases.Interfaces;
using Data.Repositories;
using Domain.UseCases;


namespace CompositionRoot
{
    public static class InversifyConfig
    {
        public static IServiceCollection AddCompositionRoot(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IListadoPersonasRepository, PersonasRepositoryAzure>();
            services.AddScoped<IListadoPersonasUseCases, GetListadoPersonasUC>();

            return services;
        }
    }
}