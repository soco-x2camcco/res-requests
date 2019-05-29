using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Requests.NationalWeatherServiceData
{
    public class GraphEntry
    {
        [JsonProperty(PropertyName = "@id")]
        public string UrlId { get; set; }
        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }
        public string Id { get; set; }
        public string areaDesc { get; set; }
        public string geometry { get; set; }
        public Geocode geocode { get; set; }
        public List<string> affectedZones { get; set; }
        public List<object> references { get; set; }
        public DateTime sent { get; set; }
        public DateTime effective { get; set; }
        public DateTime? onset { get; set; }
        public DateTime expires { get; set; }
        public DateTime? ends { get; set; }
        public string status { get; set; }
        public string messageType { get; set; }
        public string category { get; set; }
        public string severity { get; set; }
        public string certainty { get; set; }
        public string urgency { get; set; }
        public string @event { get; set; }
        public string sender { get; set; }
        public string senderName { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public string instruction { get; set; }
        public string response { get; set; }
        public Parameters parameters { get; set; }
    }
}
