using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{
    [AddInstrumentation("device")]
    public partial class Device
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("temperature")]
        [Metric("Gauge", "temperature", labels: new[] { "rig", "device" })]
        public double Temperature { get; set; }

        [JsonProperty("load")]
        [Metric("Gauge", "load", labels: new[] { "rig", "device" })]
        public double Load { get; set; }

        [JsonProperty("revolutionsPerMinute")]
        [Metric("Gauge", "rpm", labels: new[] { "rig", "device" })]
        public double RevolutionsPerMinute { get; set; }

        [JsonProperty("revolutionsPerMinutePercentage")]
        [Metric("Gauge", "rpm_percent", labels: new[] { "rig", "device" })]
        public double RevolutionsPerMinutePercentage { get; set; }

        [JsonProperty("powerUsage")]
        [Metric("Gauge", "power_usage", labels: new[] { "rig", "device" })]
        public double PowerUsage { get; set; }

        [JsonProperty("speeds")]
        public List<DeviceSpeed> Speeds { get; set; }

        [JsonProperty("nhqm")]
        public string Nhqm { get; set; }
    }


}
