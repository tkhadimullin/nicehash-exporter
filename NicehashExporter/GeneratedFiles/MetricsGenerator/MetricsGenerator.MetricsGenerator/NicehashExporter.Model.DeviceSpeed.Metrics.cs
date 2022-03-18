using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class DeviceSpeed {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_algo_speed", Metrics.CreateGauge($"{prefix}_algo_speed", "speed", "rig", "device", "algo") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, DeviceSpeed data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_algo_speed"] as Gauge).WithLabels(extraLabels.ToArray()).Set(double.Parse(data.Speed));
}


}}
