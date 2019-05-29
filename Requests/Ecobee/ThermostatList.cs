using System.Collections.Generic;

namespace Requests.Ecobee
{
    public class ThermostatList
    {
        public string identifier { get; set; }
        public string name { get; set; }
        public string thermostatRev { get; set; }
        public bool isRegistered { get; set; }
        public string modelNumber { get; set; }
        public string brand { get; set; }
        public string features { get; set; }
        public string lastModified { get; set; }
        public string thermostatTime { get; set; }
        public string utcTime { get; set; }
        public List<Alert> alerts { get; set; }
        public Settings settings { get; set; }
        public Runtime runtime { get; set; }
        public List<Event> events { get; set; }
    }
}
