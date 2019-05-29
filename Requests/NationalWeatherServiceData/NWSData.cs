using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Requests.NationalWeatherServiceData
{
    public class NwsData
    {
        [JsonProperty(PropertyName = "@context")]
        public NwsContext Context { get; set; }
        [JsonProperty(PropertyName = "@graph")]
        public List<GraphEntry> Graph { get; set; }
        public string title { get; set; }
        public DateTime updated { get; set; }
    }
}
