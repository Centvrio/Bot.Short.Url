using Centvrio.Bot.Short.Url.Providers.Results;
using Centvrio.Bot.Short.Url.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Centvrio.Bot.Short.Url.Providers
{
    public class BitlyShortUrlProvider : IShortUrlProvider
    {
        private readonly External externalSettings;
        private readonly RestClient rest;

        public BitlyShortUrlProvider(IOptions<External> external, RestClient rest)
        {
            externalSettings = external.Value;
            this.rest = rest;
        }

        public string Message => "Bitly Url Shortener";

        public string Name => "Bitly";

        public async Task<ShortenResult> Shorten(string longUrl)
        {
            BitlyShortenResult result = await ShortenInternal(longUrl);
            return new ShortenResult
            {
                ShortUrl = result?.ShortUrl,
                LongUrl = result?.LongUrl
            };
        }

        private async Task<BitlyShortenResult> ShortenInternal(string longUrl)
        {
            return await rest.Post<BitlyShortenResult, object>($"https://api-ssl.bitly.com/v4/shorten", new { long_url = longUrl }, externalSettings.Bitly.AccessToken);
        }
    }
}
