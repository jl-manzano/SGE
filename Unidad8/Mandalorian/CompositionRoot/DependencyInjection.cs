using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Domain.Repositories;
using Domain.Interfaces;
using Domain.UseCases;
using Data.Repositories;

namespace CompositionRoot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Domain Use Cases
            services.AddScoped<IListadoMisionesUseCase, ListadoMisionesUseCase>();
            // Data Repositories
            services.AddScoped<IListadoMisiones, ListadoMisiones>();
            return services;
        }
    }
}
