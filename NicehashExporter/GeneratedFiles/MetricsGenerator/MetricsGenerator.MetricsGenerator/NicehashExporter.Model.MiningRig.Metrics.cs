using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class MiningRig {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_rig_unpaid_amount", Metrics.CreateGauge($"{prefix}_rig_unpaid_amount", "unpaid_amount", "rig") },
{$"{prefix}_rig_unpaid_amount_nzd", Metrics.CreateGauge($"{prefix}_rig_unpaid_amount_nzd", "unpaid_amount_nzd", "rig") },
{$"{prefix}_rig_unpaid_amount_usd", Metrics.CreateGauge($"{prefix}_rig_unpaid_amount_usd", "unpaid_amount_usd", "rig") },
{$"{prefix}_rig_profitability", Metrics.CreateGauge($"{prefix}_rig_profitability", "profitability", "rig") },
{$"{prefix}_rig_profitability_nzd", Metrics.CreateGauge($"{prefix}_rig_profitability_nzd", "profitability_nzd", "rig") },
{$"{prefix}_rig_profitability_usd", Metrics.CreateGauge($"{prefix}_rig_profitability_usd", "profitability_usd", "rig") },
{$"{prefix}_rig_local_profitability", Metrics.CreateGauge($"{prefix}_rig_local_profitability", "local_profitability", "rig") },
{$"{prefix}_rig_local_profitability_nzd", Metrics.CreateGauge($"{prefix}_rig_local_profitability_nzd", "local_profitability_nzd", "rig") },
{$"{prefix}_rig_local_profitability_usd", Metrics.CreateGauge($"{prefix}_rig_local_profitability_usd", "local_profitability_usd", "rig") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, MiningRig data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_rig_unpaid_amount"] as Gauge).WithLabels(extraLabels.ToArray()).Set(double.Parse(data.UnpaidAmount));
(metrics[$"{prefix}_rig_unpaid_amount_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.UnpaidAmountNZD);
(metrics[$"{prefix}_rig_unpaid_amount_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.UnpaidAmountUSD);
(metrics[$"{prefix}_rig_profitability"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.Profitability);
(metrics[$"{prefix}_rig_profitability_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.ProfitabilityNZD);
(metrics[$"{prefix}_rig_profitability_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.ProfitabilityUSD);
(metrics[$"{prefix}_rig_local_profitability"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.LocalProfitability);
(metrics[$"{prefix}_rig_local_profitability_nzd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.LocalProfitabilityNZD);
(metrics[$"{prefix}_rig_local_profitability_usd"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.LocalProfitabilityUSD);
}


}}
