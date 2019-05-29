using System.Collections.Generic;

namespace Requests.Ecobee
{
    public class Runtime
    {
        public string runtimeRev { get; set; }
        public bool connected { get; set; }
        public string firstConnected { get; set; }
        public string connectDateTime { get; set; }
        public string disconnectDateTime { get; set; }
        public string lastModified { get; set; }
        public string lastStatusModified { get; set; }
        public string runtimeDate { get; set; }
        public int runtimeInterval { get; set; }
        public int actualTemperature { get; set; }
        public int actualHumidity { get; set; }
        public int rawTemperature { get; set; }
        public int showIconMode { get; set; }
        public int desiredHeat { get; set; }
        public int desiredCool { get; set; }
        public int desiredHumidity { get; set; }
        public int desiredDehumidity { get; set; }
        public string desiredFanMode { get; set; }
        public List<int> desiredHeatRange { get; set; }
        public List<int> desiredCoolRange { get; set; }
    }
}
