using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{

    [AddInstrumentation]
    public partial class NhRigResponse
    {
        [JsonProperty("totalRigs")]
        public int TotalRigs { get; set; }

        [JsonProperty("totalProfitability")]
        [Metric("Gauge", "total_profitability")]
        public double TotalProfitability { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "total_profitability_nzd")]
        public double TotalProfitabilityNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "total_profitability_usd")]
        public double TotalProfitabilityUSD { get; set; }

        [JsonProperty("groupPowerMode")]
        public string GroupPowerMode { get; set; }
        
        [JsonProperty("totalDevices")]
        [Metric("Gauge", "active_devices")]
        public int TotalDevices { get; set; }

        [JsonProperty("unpaidAmount")]
        [Metric("Gauge", "unpaid_amount", valuePath: "double.Parse(data.UnpaidAmount)")]
        public string UnpaidAmount { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "unpaid_amount_usd")]
        public double UnpaidAmountUSD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "unpaid_amount_nzd")]
        public double UnpaidAmountNZD { get; set; }

        [JsonProperty("btcAddress")]
        public string BtcAddress { get; set; }

        [JsonProperty("nextPayoutTimestamp")]
        public DateTime NextPayoutTimestamp { get; set; }

        [JsonProperty("lastPayoutTimestamp")]
        public DateTime LastPayoutTimestamp { get; set; }

        [JsonProperty("miningRigs")]
        public List<MiningRig> MiningRigs { get; set; }

        [JsonProperty("totalProfitabilityLocal")]
        [Metric("Gauge", "total_profitability_local")]
        public double TotalProfitabilityLocal { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "total_profitability_local_nzd")]
        public double TotalProfitabilityLocalNZD { get; set; }
        [JsonIgnore]
        [Metric("Gauge", "total_profitability_local_usd")]
        public double TotalProfitabilityLocalUSD { get; set; }
    }


}
