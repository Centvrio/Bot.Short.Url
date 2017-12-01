using Centvrio.Bot.Short.Url.Cryptography;
using Centvrio.Bot.Short.Url.Extensions;
using Centvrio.Bot.Short.Url.Providers;
using Centvrio.Bot.Short.Url.Providers.Results;
using Centvrio.Emoji;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Centvrio.Bot.Short.Url.Commands
{
    public class ShortenCommand : ICommand
    {
        private readonly IEnumerable<IShortUrlProvider> shorteners;
        private readonly IStringLocalizer<ShortenCommand> localizer;

        public ShortenCommand(IServiceProvider serviceProvider, IStringLocalizer<ShortenCommand> localizer)
        {
            shorteners = serviceProvider.GetServices<IShortUrlProvider>();
            this.localizer = localizer;
        }

        public bool CanExecute(Update update) => !string.IsNullOrEmpty(update?.Message?.Text) && update?.Message?.Chat != null;

        public async Task Execute(TelegramBotClient client, Update update)
        {
            Message message = update.Message;
            long chatId = message.Chat.Id;
            if (message.Type == MessageType.Text)
            {
                User user = update.GetUser();
                await client.SendChatActionAsync(chatId, ChatAction.Typing);
                IStringLocalizer loc = localizer.WithCulture(new CultureInfo(user.LanguageCode));
                var urls = message.EntityValues
                    .Where((entity, index) => message.Entities[index].Type == MessageEntityType.Url)
                    .ToList();
                string answer = string.Format(loc["NotUrlMessage"], FaceNeutral.Confused);

                if (urls.Count == 1)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (IShortUrlProvider shortener in shorteners)
                    {
                        string rawKey = $"{user.Id}{shortener.Name}{message.Text}";
                        byte[] rawKeyBytes = Encoding.UTF8.GetBytes(rawKey);
                        ShortenResult result = await shortener.Shorten(message.Text);
                        if (!string.IsNullOrEmpty(result.ShortUrl))
                        {
                            stringBuilder.AppendFormat(loc["OkMessage"], shortener.Message, result.ShortUrl);
                        }
                        else
                        {
                            stringBuilder.AppendFormat(loc["FailMessage"], shortener.Message, FaceNegative.Worried);
                        }
                        stringBuilder.AppendLine().AppendLine();
                    }
                    answer = stringBuilder.ToString();
                }
                else if (urls.Count > 1)
                {
                    answer = string.Format(loc["ManyUrlsMessage"], urls.Count, FaceNegative.Crying);
                }
                await client.SendTextMessageAsync(chatId, answer, ParseMode.Markdown, disableWebPagePreview: true);
            }
        }
    }
}