using Microsoft.Extensions.Configuration;

namespace NicehashExporter.API
{
    public class NicehashClient
    {
        private readonly HttpClient _client;        
        private readonly string _apiSecret;
        private readonly string _apiKey;
        private readonly string _orgId;

        public NicehashClient(IConfiguration configuration) {

            _apiSecret = configuration.GetValue("SEC", "");
            _apiKey = configuration.GetValue("KEY", "");
            _orgId = configuration.GetValue("ORG", "");

            var baseUrl = configuration.GetValue("API", "https://api2.nicehash.com");
            _client = new HttpClient() { BaseAddress = new Uri(baseUrl) };            
            _client.DefaultRequestHeaders.Add("X-Organization-Id", _orgId);
        }

        public async Task<string> Get(string url)
        {
            return await Get(url, true, null);
        }

        private static string GetPath(string url)
        {
            var arrSplit = url.Split('?');
            return arrSplit[0];
        }
        private static string GetQuery(string url)
        {
            var arrSplit = url.Split('?');

            if (arrSplit.Length == 1)
            {
                return null;
            }
            else
            {
                return arrSplit[1];
            }
        }

        public async Task<string> Get(string url, bool auth, string time)
        {            
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                if (auth)
                {
                    string nonce = Guid.NewGuid().ToString();
                    string digest = ApiClientHelpers.HashBySegments(_apiSecret, _apiKey, time, nonce, _orgId, "GET", GetPath(url), GetQuery(url), null);

                    request.Headers.Add("X-Request-Id", Guid.NewGuid().ToString());
                    request.Headers.Add("X-Time", time);
                    request.Headers.Add("X-Nonce", nonce);
                    request.Headers.Add("X-Auth", $"{_apiKey}:{digest}");

                }
                
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                

                return content;
            }                        
        }
    }
}
