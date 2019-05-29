using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.NationalWeatherServiceData
{
    public class LatLongPoint
    {
        private double longitude;
        private double latitude;

        public LatLongPoint(double latitude, double longitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public double Longitude { get => longitude; }
        public double Latitude { get => latitude; }
    }
}
