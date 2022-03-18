using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{
    [AddInstrumentation]
    public partial class NhAccountResponse
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("totalBalance")]
        [Metric("Gauge", "total_balance")]
        public double TotalBalance { get; set; }

        [JsonProperty("available")]
        public string Available { get; set; }

        [JsonProperty("debt")]
        public string Debt { get; set; }

        [JsonProperty("pending")]
        [Metric("Gauge", "total_pending")]
        public double Pending { get; set; }

        [JsonProperty("btcRate")]
        public double BtcRate { get; set; }
        
        [JsonProperty("fiatRate")]
        public double FiatRate { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "total_balance_usd")]
        public double TotalBalanceUSD { get; set; }

        [JsonIgnore]
        [Metric("Gauge", "total_balance_nzd")]
        public double TotalBalanceNZD { get; set; }
    }


}
