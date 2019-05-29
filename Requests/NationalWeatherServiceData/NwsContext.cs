using Newtonsoft.Json;

namespace Requests.NationalWeatherServiceData
{
    public class NwsContext
    {
        [JsonProperty(PropertyName ="wx")]
        public string Wx { get; set; }
        [JsonProperty(PropertyName = "@vocab")]
        public string Vocab { get; set; }
    }
}
