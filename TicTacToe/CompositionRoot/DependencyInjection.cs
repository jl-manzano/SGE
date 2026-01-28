using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Domain.Interfaces;
using TicTacToe.Domain.Services;
using TicTacToe.Domain.UseCases;

namespace TicTacToe.CompositionRoot
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddTicTacToeServices(this IServiceCollection services)
        {
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<GameService>();
            services.AddSingleton<IUseCases, UseCases>();

            return services;
        }
    }
}