using Centvrio.Bot.Short.Url.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Centvrio.Bot.Short.Url.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddSingleton<Bot>();
            return services;
        }

        public static IServiceCollection AddCommandExecutor(this IServiceCollection services)
        {
            services.AddScoped<ICommand, StartCommand>();
            services.AddScoped<ICommand, ShortenCommand>();
            services.AddScoped<CommandExecutor>();
            return services;
        }
    }
}
