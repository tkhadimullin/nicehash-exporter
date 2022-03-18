using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{
    [AddInstrumentation("algo")]
    public partial class DeviceSpeed
    {
        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("speed")]
        [Metric("Gauge", "speed", valuePath: "double.Parse(data.Speed)", labels: new[] { "rig", "device", "algo" })]
        public string Speed { get; set; }

        [JsonProperty("displaySuffix")]
        public string DisplaySuffix { get; set; }
    }


}
