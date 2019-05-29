namespace Requests.SkyCentrics.DeviceData
{
    public class Commodity
    {
        public int code { get; set; }
        public string name { get; set; }
        public string units { get; set; }
        public int estimated { get; set; }
        public int instantaneous { get; set; }
        public int cumulative { get; set; }
    }
}