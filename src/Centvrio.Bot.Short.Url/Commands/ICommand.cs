using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Centvrio.Bot.Short.Url.Commands
{
    public interface ICommand
    {
        bool CanExecute(Update update);

        Task Execute(TelegramBotClient client, Update update);
    }
}
