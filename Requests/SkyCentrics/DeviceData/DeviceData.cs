using Newtonsoft.Json;
using System.Collections.Generic;

namespace Requests.SkyCentrics.DeviceData
{
    public class DeviceData
    {
        public int device { get; set; }

        public int relay { get; set; }

        [JsonProperty(PropertyName ="override")]
        public int Override { get; set; }

        public HoldMode hold_mode { get; set; }

        public int state { get; set; }

        public Commodity commodity { get; set; }

        public List<Commodity> commodities { get; set; }

        public int power { get; set; }

        public string time { get; set; }
    }
}