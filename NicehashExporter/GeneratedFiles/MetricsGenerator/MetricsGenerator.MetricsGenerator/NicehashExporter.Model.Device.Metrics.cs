using Newtonsoft.Json;
using TrexExporter.Metadata;

using System; 
using System.Collections.Generic;
using Prometheus; 


namespace NicehashExporter.Model { public partial class Device {

public static Dictionary<string, Collector> GetMetrics(string prefix)
                {
                    var result = new Dictionary<string, Collector>
                    {
{$"{prefix}_device_temperature", Metrics.CreateGauge($"{prefix}_device_temperature", "temperature", "rig", "device") },
{$"{prefix}_device_load", Metrics.CreateGauge($"{prefix}_device_load", "load", "rig", "device") },
{$"{prefix}_device_rpm", Metrics.CreateGauge($"{prefix}_device_rpm", "rpm", "rig", "device") },
{$"{prefix}_device_rpm_percent", Metrics.CreateGauge($"{prefix}_device_rpm_percent", "rpm_percent", "rig", "device") },
{$"{prefix}_device_power_usage", Metrics.CreateGauge($"{prefix}_device_power_usage", "power_usage", "rig", "device") },
};
                            return result;
                        }

public static void UpdateMetrics(string prefix, MetricCollection metrics, Device data, List<string> extraLabels = null) {
if(extraLabels == null) { 
                                    extraLabels = new List<string>();
                                }
(metrics[$"{prefix}_device_temperature"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.Temperature);
(metrics[$"{prefix}_device_load"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.Load);
(metrics[$"{prefix}_device_rpm"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.RevolutionsPerMinute);
(metrics[$"{prefix}_device_rpm_percent"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.RevolutionsPerMinutePercentage);
(metrics[$"{prefix}_device_power_usage"] as Gauge).WithLabels(extraLabels.ToArray()).Set(data.PowerUsage);
}


}}
