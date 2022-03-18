using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class Stat {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_algo_unpaid_amount", Metrics.CreateGauge($"{prefix}_algo_unpaid_amount", "unpaid_amount", "rig", "algo") },
{$"{prefix}_algo_unpaid_amount_nzd", Metrics.CreateGauge($"{prefix}_algo_unpaid_amount_nzd", "unpaid_amount_nzd", "rig", "algo") },
{$"{prefix}_algo_unpaid_amount_usd", Metrics.CreateGauge($"{prefix}_algo_unpaid_amount_usd", "unpaid_amount_usd", "rig", "algo") },
{$"{prefix}_algo_difficulty", Metrics.CreateGauge($"{prefix}_algo_difficulty", "difficulty", "rig", "algo") },
{$"{prefix}_algo_speed_accepted", Metrics.CreateGauge($"{prefix}_algo_speed_accepted", "speed_accepted", "rig", "algo") },
{$"{prefix}_algo_speed_rejected", Metrics.CreateGauge($"{prefix}_algo_speed_rejected", "speed_rejected", "rig", "algo") },
{$"{prefix}_algo_profitability", Metrics.CreateGauge($"{prefix}_algo_profitability", "profitability", "rig", "algo") },
{$"{prefix}_algo_profitability_nzd", Metrics.CreateGauge($"{prefix}_algo_profitability_nzd", "profitability_nzd", "rig", "algo") },
{$"{prefix}_algo_profitability_usd", Metrics.CreateGauge($"{prefix}_algo_profitability_usd", "profitability_usd", "rig", "algo") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, Stat data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_algo_unpaid_amount"] as Gauge).WithLabels(extraLabels.ToArray()).Set(double.Parse(data.UnpaidAmount));
(metrics[$"{prefix}_algo_unpaid_amount_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.UnpaidAmountNZD);
(metrics[$"{prefix}_algo_unpaid_amount_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.UnpaidAmountUSD);
(metrics[$"{prefix}_algo_difficulty"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.Difficulty);
(metrics[$"{prefix}_algo_speed_accepted"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.SpeedAccepted);
(metrics[$"{prefix}_algo_speed_rejected"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.SpeedRejectedTotal);
(metrics[$"{prefix}_algo_profitability"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.Profitability);
(metrics[$"{prefix}_algo_profitability_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.ProfitabilityNZD);
(metrics[$"{prefix}_algo_profitability_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.ProfitabilityUSD);
}


}}
