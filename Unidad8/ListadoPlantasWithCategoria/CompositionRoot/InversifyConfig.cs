using Data.Repositories;
using Domain.Interfaces;
using Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CompositionRoot
{
    public static class InversifyConfig
    {
        public static IServiceCollection AddCompositionRoot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPlantaRepository, PlantaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            services.AddScoped<IPlantaUseCases, PlantaUseCases>();
            services.AddScoped<ICategoriaUseCases, CategoriaUseCases>();

            return services;
        }
    }
}
