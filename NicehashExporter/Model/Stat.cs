using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{
    [AddInstrumentation("algo")]
    public partial class Stat
    {
        [JsonProperty("statsTime")]
        public object StatsTime { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("algorithm")]
        public Algorithm Algorithm { get; set; }

        [JsonProperty("unpaidAmount")]
        [Metric("Gauge", "unpaid_amount", valuePath: "double.Parse(data.UnpaidAmount)", labels: new[] { "rig", "algo" })]
        public string UnpaidAmount { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "unpaid_amount_nzd", labels: new[] { "rig", "algo" })]
        public double UnpaidAmountNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "unpaid_amount_usd", labels: new[] { "rig", "algo" })]
        public double UnpaidAmountUSD { get; set; }

        [JsonProperty("difficulty")]
        [Metric("Gauge", "difficulty", labels: new[] { "rig", "algo" })]
        public double Difficulty { get; set; }

        [JsonProperty("proxyId")]
        public int ProxyId { get; set; }

        [JsonProperty("timeConnected")]
        public object TimeConnected { get; set; }

        [JsonProperty("xnsub")]
        public bool Xnsub { get; set; }
        
        [JsonProperty("speedAccepted")]
        [Metric("Gauge", "speed_accepted", labels: new[] { "rig", "algo" })]
        public double SpeedAccepted { get; set; }

        [JsonProperty("speedRejectedR1Target")]
        public double SpeedRejectedR1Target { get; set; }

        [JsonProperty("speedRejectedR2Stale")]
        public double SpeedRejectedR2Stale { get; set; }

        [JsonProperty("speedRejectedR3Duplicate")]
        public double SpeedRejectedR3Duplicate { get; set; }

        [JsonProperty("speedRejectedR4NTime")]
        public double SpeedRejectedR4NTime { get; set; }

        [JsonProperty("speedRejectedR5Other")]
        public double SpeedRejectedR5Other { get; set; }

        [JsonProperty("speedRejectedTotal")]
        [Metric("Gauge", "speed_rejected", labels: new[] { "rig", "algo" })]
        public double SpeedRejectedTotal { get; set; }

        [JsonProperty("profitability")]
        [Metric("Gauge", "profitability", labels: new[] { "rig", "algo" })]
        public double Profitability { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "profitability_nzd", labels: new[] { "rig", "algo" })]
        public double ProfitabilityNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "profitability_usd", labels: new[] { "rig", "algo" })]
        public double ProfitabilityUSD { get; set; }
    }


}
