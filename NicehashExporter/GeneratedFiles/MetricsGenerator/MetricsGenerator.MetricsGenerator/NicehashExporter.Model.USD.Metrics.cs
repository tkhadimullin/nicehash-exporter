using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class USD {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_rate_float", Metrics.CreateGauge($"{prefix}_rate_float", "rate_float", "currency") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, USD data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_rate_float"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.RateFloat);
}


}}
