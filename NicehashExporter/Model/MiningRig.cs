using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{
    [AddInstrumentation("rig")]
    public partial class MiningRig
    {
        [JsonProperty("rigId")]
        public string RigId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("statusTime")]
        public object StatusTime { get; set; }

        [JsonProperty("joinTime")]
        public int JoinTime { get; set; }

        [JsonProperty("minerStatus")]
        public string MinerStatus { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("unpaidAmount")]
        [Metric("Gauge", "unpaid_amount", valuePath: "double.Parse(data.UnpaidAmount)", labels: new[] { "rig" })]
        public string UnpaidAmount { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "unpaid_amount_nzd", labels: new[] { "rig" })]
        public double UnpaidAmountNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "unpaid_amount_usd", labels: new[] { "rig" })]
        public double UnpaidAmountUSD { get; set; }

        [JsonProperty("softwareVersions")]
        public string SoftwareVersions { get; set; }

        [JsonProperty("devices")]
        public List<Device> Devices { get; set; }

        [JsonProperty("cpuMiningEnabled")]
        public bool CpuMiningEnabled { get; set; }

        [JsonProperty("cpuExists")]
        public bool CpuExists { get; set; }

        [JsonProperty("stats")]
        public List<Stat> Stats { get; set; }

        [JsonProperty("profitability")]
        [Metric("Gauge", "profitability", labels: new[] { "rig" })]
        public double Profitability { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "profitability_nzd", labels: new[] { "rig" })]
        public double ProfitabilityNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "profitability_usd", labels: new[] { "rig" })]
        public double ProfitabilityUSD { get; set; }

        [JsonProperty("localProfitability")]
        [Metric("Gauge", "local_profitability", labels: new[] { "rig" })]
        public double LocalProfitability { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "local_profitability_nzd", labels: new[] { "rig" })]
        public double LocalProfitabilityNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "local_profitability_usd", labels: new[] { "rig" })]
        public double LocalProfitabilityUSD { get; set; }

        [JsonProperty("rigPowerMode")]
        public string RigPowerMode { get; set; }
    }


}
