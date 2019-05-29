using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Requests.NationalWeatherServiceData
{
    public class Parameters
    {
        public List<string> NWSheadline { get; set; }
        [JsonProperty(PropertyName = "EAS-ORG")]
        public List<string> EASOrg { get; set; }
        public List<string> PIL { get; set; }
        public List<string> BLOCKCHANNEL { get; set; }
        public List<string> VTEC { get; set; }
        public List<DateTime?> eventEndingTime { get; set; }
        public List<string> HazardType { get; set; }
    }
}
