using Requests.NationalWeatherServiceData.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.NationalWeatherServiceData
{
    public class NWSForecastEndpoints
    {
        public NWSForecastEndpoints(PointsResponse pointsResponse)
        {
            this.forecast = pointsResponse.forecast;
            this.forecastHourly = pointsResponse.forecastHourly;
            this.forecastGridData = pointsResponse.forecastGridData;
        }
        public string forecast;
        public string forecastHourly;
        public string forecastGridData;
    }
}
