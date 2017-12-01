using Centvrio.Bot.Short.Url.Providers.Results;
using System.Threading.Tasks;

namespace Centvrio.Bot.Short.Url.Providers
{
    public interface IShortUrlProvider
    {
        string Name { get; }

        string Message { get; }

        Task<ShortenResult> Shorten(string longUrl);
    }
}
