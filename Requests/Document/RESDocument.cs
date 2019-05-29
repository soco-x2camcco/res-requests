using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Document
{
    public class RESDocument
    {
#pragma warning disable IDE1006 // Naming Styles
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public dynamic envelope { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string device_id { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string data_category { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string data_type { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string participant_id { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string subscription_id { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string partition_key { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string timestamp_utc { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string agent_id { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string envelope_content { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string definition_id { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public dynamic metadata { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
