using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Centvrio.Bot.Short.Url.Extensions
{
    public static class UpdateExtensions
    {
        public static User GetUser(this Update self)
        {
            if (self != null)
            {
                switch (self.Type)
                {
                    case UpdateType.Message:
                        return self.Message.From;
                    case UpdateType.EditedMessage:
                        return self.EditedMessage.From;
                    case UpdateType.InlineQuery:
                        return self.InlineQuery.From;
                    case UpdateType.CallbackQuery:
                        return self.CallbackQuery.From;
                }
            }
            return null;
        }
    }
}
