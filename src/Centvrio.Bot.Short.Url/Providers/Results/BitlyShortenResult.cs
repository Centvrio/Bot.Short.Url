using Newtonsoft.Json;

namespace Centvrio.Bot.Short.Url.Providers.Results
{
    public class BitlyShortenResult : IFaultyResult
    {
        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonIgnore]
        bool IFaultyResult.Failed => StatusCode != 200;

        [JsonIgnore]
        string IFaultyResult.FailMessage => Message;

        [JsonProperty("link")]
        public string ShortUrl { get; set; }

        [JsonProperty("long_url")]
        public string LongUrl { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
