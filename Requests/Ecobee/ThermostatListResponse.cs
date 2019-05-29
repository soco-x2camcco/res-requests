using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Ecobee
{

    public class ThermostatListResponse
    {
        public Page page { get; set; }
        public List<ThermostatList> thermostatList { get; set; }
        public Status status { get; set; }
    }
}
