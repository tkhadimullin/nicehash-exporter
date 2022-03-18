using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class NhRigResponse {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_total_profitability", Metrics.CreateGauge($"{prefix}_total_profitability", "total_profitability") },
{$"{prefix}_total_profitability_nzd", Metrics.CreateGauge($"{prefix}_total_profitability_nzd", "total_profitability_nzd") },
{$"{prefix}_total_profitability_usd", Metrics.CreateGauge($"{prefix}_total_profitability_usd", "total_profitability_usd") },
{$"{prefix}_active_devices", Metrics.CreateGauge($"{prefix}_active_devices", "active_devices") },
{$"{prefix}_unpaid_amount", Metrics.CreateGauge($"{prefix}_unpaid_amount", "unpaid_amount") },
{$"{prefix}_unpaid_amount_usd", Metrics.CreateGauge($"{prefix}_unpaid_amount_usd", "unpaid_amount_usd") },
{$"{prefix}_unpaid_amount_nzd", Metrics.CreateGauge($"{prefix}_unpaid_amount_nzd", "unpaid_amount_nzd") },
{$"{prefix}_total_profitability_local", Metrics.CreateGauge($"{prefix}_total_profitability_local", "total_profitability_local") },
{$"{prefix}_total_profitability_local_nzd", Metrics.CreateGauge($"{prefix}_total_profitability_local_nzd", "total_profitability_local_nzd") },
{$"{prefix}_total_profitability_local_usd", Metrics.CreateGauge($"{prefix}_total_profitability_local_usd", "total_profitability_local_usd") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, NhRigResponse data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_total_profitability"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalProfitability);
(metrics[$"{prefix}_total_profitability_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalProfitabilityNZD);
(metrics[$"{prefix}_total_profitability_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalProfitabilityUSD);
(metrics[$"{prefix}_active_devices"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalDevices);
(metrics[$"{prefix}_unpaid_amount"] as Gauge).WithLabels(extraLabels.ToArray()).Set(double.Parse(data.UnpaidAmount));
(metrics[$"{prefix}_unpaid_amount_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.UnpaidAmountUSD);
(metrics[$"{prefix}_unpaid_amount_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.UnpaidAmountNZD);
(metrics[$"{prefix}_total_profitability_local"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalProfitabilityLocal);
(metrics[$"{prefix}_total_profitability_local_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalProfitabilityLocalNZD);
(metrics[$"{prefix}_total_profitability_local_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.TotalProfitabilityLocalUSD);
}


}}
