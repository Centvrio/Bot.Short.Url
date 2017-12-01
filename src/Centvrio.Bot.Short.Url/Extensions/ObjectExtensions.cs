using Newtonsoft.Json;

namespace Centvrio.Bot.Short.Url.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson<T>(this T self) where T : class
        {
            if (self != null)
            {
                return JsonConvert.SerializeObject(self);
            }
            return null;
        }

        public static T ToObject<T>(this string self) where T : class
        {
            if (!string.IsNullOrEmpty(self))
            {
                return JsonConvert.DeserializeObject<T>(self);
            }
            return null;
        }
    }
}
