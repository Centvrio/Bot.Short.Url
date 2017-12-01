using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Centvrio.Bot.Short.Url.Commands
{
    public class CommandExecutor
    {
        private readonly IReadOnlyList<ICommand> commands;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
            commands = serviceProvider.GetServices<ICommand>().ToList().AsReadOnly();
        }

        public async Task Execute(TelegramBotClient client, Update update)
        {
            ICommand command = commands.FirstOrDefault(cmd => cmd.CanExecute(update));
            if (command != null)
            {
                await command.Execute(client, update);
            }
        }
    }
}
