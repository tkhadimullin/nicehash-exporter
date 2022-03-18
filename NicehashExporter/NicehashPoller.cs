using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NicehashExporter.API;
using NicehashExporter.Model;

namespace NicehashExporter
{
    public class NicehashPoller: IHostedService
    {
        private bool _stopping;
        protected readonly IConfiguration _configuration;
        private readonly MetricCollection _metrics;
        private readonly string _prefix;
        private readonly NicehashClient _NiceHashClient;
        private readonly CoinDeskPoller _coinDesk;
        private readonly HttpClient _httpClient;

        public NicehashPoller(IConfiguration configuration, MetricCollection metrics, NicehashClient client, CoinDeskPoller coinDesk)
        {
            _configuration = configuration;
            _metrics = metrics;
            _prefix = configuration.GetValue("prefix", "nicehash");
            _NiceHashClient = client;
            _coinDesk = coinDesk;
            _httpClient = new HttpClient();

            foreach ((var key, var value) in NhRigResponse.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
            foreach ((var key, var value) in MiningRig.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
            foreach ((var key, var value) in Device.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
            foreach ((var key, var value) in DeviceSpeed.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
            foreach ((var key, var value) in Stat.GetMetrics(_prefix)) _metrics.TryAdd(key, value);

            foreach ((var key, var value) in NhAccountResponse.GetMetrics(_prefix)) _metrics.TryAdd(key, value);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () => {
                while (!cancellationToken.IsCancellationRequested && !_stopping)
                {
                    try
                    {
                        var t = await _NiceHashClient.Get("/api/v2/time", false, null);
                        var time = JsonConvert.DeserializeObject<dynamic>(t);

                        var rigsResponse = await _NiceHashClient.Get("/main/api/v2/mining/rigs2?status=Mining", true, (string)time.serverTime);
                        var accountResponse = await _NiceHashClient.Get("/main/api/v2/accounting/account2/BTC", true, (string)time.serverTime);
                        
                        var nhRigData = JsonConvert.DeserializeObject<NhRigResponse>(rigsResponse);
                        var accountData = JsonConvert.DeserializeObject<NhAccountResponse>(accountResponse);
                        
                        accountData.TotalBalanceNZD = accountData.TotalBalance * _coinDesk.GetRate("NZD");
                        accountData.TotalBalanceUSD = accountData.TotalBalance * _coinDesk.GetRate("USD");
                        nhRigData.TotalProfitabilityNZD = nhRigData.TotalProfitability * _coinDesk.GetRate("NZD");
                        nhRigData.TotalProfitabilityUSD = nhRigData.TotalProfitability * _coinDesk.GetRate("USD");
                        nhRigData.TotalProfitabilityLocalNZD = nhRigData.TotalProfitabilityLocal * _coinDesk.GetRate("NZD");
                        nhRigData.TotalProfitabilityLocalUSD = nhRigData.TotalProfitabilityLocal * _coinDesk.GetRate("USD");
                        nhRigData.UnpaidAmountNZD = double.Parse(nhRigData.UnpaidAmount) * _coinDesk.GetRate("NZD");
                        nhRigData.UnpaidAmountUSD = double.Parse(nhRigData.UnpaidAmount) * _coinDesk.GetRate("USD");                        

                        
                        NhAccountResponse.UpdateMetrics(_prefix, _metrics, accountData);
                        NhRigResponse.UpdateMetrics(_prefix, _metrics, nhRigData);

                        foreach (var rig in nhRigData.MiningRigs)
                        {
                            rig.ProfitabilityNZD = rig.Profitability * _coinDesk.GetRate("NZD");
                            rig.ProfitabilityUSD = rig.Profitability * _coinDesk.GetRate("USD");
                            rig.LocalProfitabilityNZD = rig.LocalProfitability * _coinDesk.GetRate("NZD");
                            rig.LocalProfitabilityUSD = rig.LocalProfitability * _coinDesk.GetRate("USD");
                            rig.UnpaidAmountNZD = double.Parse(rig.UnpaidAmount) * _coinDesk.GetRate("NZD");
                            rig.UnpaidAmountUSD = double.Parse(rig.UnpaidAmount) * _coinDesk.GetRate("USD");

                            MiningRig.UpdateMetrics(_prefix, _metrics, rig, new List<string> {
                                rig.Name
                            });

                            foreach (var device in (rig.Devices ?? new List<Device>()).Where(d => d != null))
                            {
                                Device.UpdateMetrics(_prefix, _metrics, device, new List<string> {
                                    rig.Name,
                                    device.Name
                                });
                                foreach (var deviceSpeed in device.Speeds) {
                                    DeviceSpeed.UpdateMetrics(_prefix, _metrics, deviceSpeed, new List<string> {
                                        rig.Name,
                                        device.Name,
                                        deviceSpeed.Algorithm
                                    });
                                }
                            }

                            foreach (var stat in (rig.Stats ?? new List<Stat>()).Where(s => s != null))
                            {
                                stat.ProfitabilityNZD = stat.Profitability * _coinDesk.GetRate("NZD");
                                stat.ProfitabilityUSD = stat.Profitability * _coinDesk.GetRate("USD");
                                stat.UnpaidAmountNZD = double.Parse(stat.UnpaidAmount) * _coinDesk.GetRate("NZD");
                                stat.UnpaidAmountUSD = double.Parse(stat.UnpaidAmount) * _coinDesk.GetRate("USD");

                                Stat.UpdateMetrics(_prefix, _metrics, stat, new List<string> {
                                    rig.Name,
                                    stat.Algorithm.Description
                                });
                            }

                        }

                        Thread.Sleep(_configuration.GetValue<int>("pollInterval", 60000));
                    }
                    catch (Exception ex)
                    {
                        // TODO: error handling
                    }
                }
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _stopping = true;
            return Task.CompletedTask;
        }
    }
}
