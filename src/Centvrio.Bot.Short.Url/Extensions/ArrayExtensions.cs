using System.Text;

namespace Centvrio.Bot.Short.Url.Extensions
{
    public static class ArrayExtensions
    {
        public static string ToHex(this byte[] bytes)
        {
            if ((bytes?.Length ?? 0) > 0)
            {
                var sb = new StringBuilder(bytes.Length*2);
                foreach(byte b in bytes)
                {
                    sb.AppendFormat("{0:x2}", b);
                }
                return sb.ToString();
            }
            return default;
        }
    }
}
