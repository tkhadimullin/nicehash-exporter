using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class NhAccountResponse {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_total_balance", Metrics.CreateGauge($"{prefix}_total_balance", "total_balance") },
{$"{prefix}_total_pending", Metrics.CreateGauge($"{prefix}_total_pending", "total_pending") },
{$"{prefix}_total_balance_usd", Metrics.CreateGauge($"{prefix}_total_balance_usd", "total_balance_usd") },
{$"{prefix}_total_balance_nzd", Metrics.CreateGauge($"{prefix}_total_balance_nzd", "total_balance_nzd") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, NhAccountResponse data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_total_balance"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalBalance);
(metrics[$"{prefix}_total_pending"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.Pending);
(metrics[$"{prefix}_total_balance_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalBalanceUSD);
(metrics[$"{prefix}_total_balance_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalBalanceNZD);
}


}}
