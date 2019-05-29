using System.Collections.Generic;

namespace Requests.Sensibo.Device
{
    public class AcStateCollection
    {
        public string status { get; set; }
        public bool moreResults { get; set; }
        public List<Result> result { get; set; }
    }
}
