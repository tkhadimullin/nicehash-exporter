using Newtonsoft.Json;
using TrexExporter.Metadata;

namespace NicehashExporter.Model
{

    [AddInstrumentation]
    public partial class USD
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate_float")]
        [Metric("Gauge", labels: new[] { "currency" })]
        public double RateFloat { get; set; }
    }

    [AddInstrumentation]
    public partial class NZD
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate_float")]
        [Metric("Gauge", labels: new[] { "currency" })]
        public double RateFloat { get; set; }
    }

    public class Bpi
    {
        [JsonProperty("USD")]
        public USD USD { get; set; }

        [JsonProperty("NZD")]
        public NZD NZD { get; set; }
    }
    
    public class CoindeskCurrencyRates
    {
        [JsonProperty("bpi")]
        public Bpi Bpi { get; set; }
    }
}
