using System.Globalization;
using System.Threading.Tasks;
using Centvrio.Bot.Short.Url.Extensions;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Centvrio.Bot.Short.Url.Commands
{
    public class StartCommand : ICommand
    {
        private readonly IStringLocalizer<StartCommand> localizer;

        public StartCommand(IStringLocalizer<StartCommand> localizer)
        {
            this.localizer = localizer;
        }

        public bool CanExecute(Update update) => update?.Message?.Text == "/start" && update?.Message?.Chat != null;

        public async Task Execute(TelegramBotClient client, Update update)
        {
            Message message = update.Message;
            long chatId = message.Chat.Id;
            if (message.Type == MessageType.Text)
            {
                await client.SendChatActionAsync(chatId, ChatAction.Typing);
                IStringLocalizer loc = localizer.WithCulture(new CultureInfo(update.GetUser().LanguageCode));
                await client.SendTextMessageAsync(chatId, string.Format(loc["StartMessage"].Value, message.Chat.FirstName));
            }
        }
    }
}
