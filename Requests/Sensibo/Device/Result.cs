using System.Collections.Generic;

namespace Requests.Sensibo.Device
{
    public class Result
    {
        public string status { get; set; }
        public string reason { get; set; }
        public AcState acState { get; set; }
        public List<object> changedProperties { get; set; }
        public string id { get; set; }
        public object failureReason { get; set; }
    }
}