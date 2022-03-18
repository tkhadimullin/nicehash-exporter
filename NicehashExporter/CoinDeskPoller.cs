using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NicehashExporter.Model;

namespace NicehashExporter
{
    public class CoinDeskPoller : IHostedService
    {
        private bool _stopping;
        protected readonly IConfiguration _configuration;
        private readonly MetricCollection _metrics;
        private readonly string _prefix;
        private readonly string _mainCurrency;
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, double> _rates;

        public CoinDeskPoller(IConfiguration configuration, MetricCollection metrics)
        {
            _configuration = configuration;
            _metrics = metrics;            
            _prefix = configuration.GetValue("coindeskPrefix", "coindesk");
            _mainCurrency = "NZD"; // TODO: configuration.GetValue("mainCurrency", "NZD").ToUpperInvariant();

            _rates = new Dictionary<string, double>() { { _mainCurrency, -1 }, { "USD", -1 } };

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.coindesk.com/v1/")
            };

            foreach ((var key, var value) in USD.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
            foreach ((var key, var value) in NZD.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
        }

        private async Task<CoindeskCurrencyRates> FetchApiData() 
        {
            var currencyRates = await _httpClient.GetStringAsync($"bpi/currentPrice/{_mainCurrency}.json");
            var currencyData = JsonConvert.DeserializeObject<CoindeskCurrencyRates>(currencyRates);

            _rates[_mainCurrency] = currencyData.Bpi.NZD.RateFloat;
            _rates["USD"] = currencyData.Bpi.USD.RateFloat;

            return currencyData;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () => {
                while (!cancellationToken.IsCancellationRequested && !_stopping)
                {
                    try
                    {
                        var currencyData = await FetchApiData();

                        USD.UpdateMetrics(_prefix, _metrics, currencyData.Bpi.USD, new List<string> { "USD" });
                        NZD.UpdateMetrics(_prefix, _metrics, currencyData.Bpi.NZD, new List<string> { "NZD" });
                        Thread.Sleep(_configuration.GetValue("pollInterval", 60000));
                    }
                    catch (Exception ex)
                    {
                        // TODO: error handling
                    }
                }
            });
            return Task.CompletedTask;
        }

        public double GetRate(string currency) {
            if (_rates[currency] < 0) {
                _ = FetchApiData().Result;
            }
            return _rates[currency];
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _stopping = true;
            return Task.CompletedTask;
        }
    }
}
