using Centvrio.Bot.Short.Url.Providers.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Centvrio.Bot.Short.Url
{
    public class RestClient
    {
        private readonly ILogger<RestClient> logger;

        public RestClient(ILogger<RestClient> logger)
        {
            this.logger = logger;
        }

        public async Task<T> Get<T>(string uri) where T : IFaultyResult
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                using (HttpResponseMessage response = await client.GetAsync(uri))
                using (HttpContent content = response.Content)
                {
                    return await ResponseHandler<T>(response, content);
                }
            }
        }

        public async Task<T> Post<T, U>(string uri, U data) where T : IFaultyResult => await Post<T, U>(uri, data, null);

        public async Task<T> Post<T, U>(string uri, U data, string bearerToken) where T : IFaultyResult
        {
            string json = JsonConvert.SerializeObject(data);
            var stringData = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(bearerToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }
                using (HttpResponseMessage response = await client.PostAsync(uri, stringData))
                using (HttpContent content = response.Content)
                {
                    return await ResponseHandler<T>(response, content);
                }
            }
        }

        private async Task<T> ResponseHandler<T>(HttpResponseMessage response, HttpContent content) where T : IFaultyResult
        {
            string json = await content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(json);
            result.StatusCode = (int)response.StatusCode;
            if (result.Failed)
            {
                logger.LogError($"Server response '{result.FailMessage}' with code {result.StatusCode}");
            }
            return result;
        }
    }
}
