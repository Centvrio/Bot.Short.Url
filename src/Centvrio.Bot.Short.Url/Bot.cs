using Centvrio.Bot.Short.Url.Settings;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Centvrio.Bot.Short.Url
{
    public class Bot
    {
        private readonly External externalSettings;

        public TelegramBotClient Client { get; private set; }

        public Bot(IOptions<External> external)
        {
            externalSettings = external.Value;
        }

        public void Init()
        {
            string hookUrl = string.Format(externalSettings.Telegram.Hook, "bots/shortener/update");
            Client = new TelegramBotClient(externalSettings.Telegram.Token);
            Client.SetWebhookAsync(hookUrl)
                .GetAwaiter()
                .GetResult();
        }
    }
}
