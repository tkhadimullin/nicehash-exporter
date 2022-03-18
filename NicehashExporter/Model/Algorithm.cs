using Newtonsoft.Json;

namespace NicehashExporter.Model
{
    public class Algorithm
    {
        [JsonProperty("enumName")]
        public string EnumName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }


}
