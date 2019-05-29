using Newtonsoft.Json;
using Requests.Document;
using Requests.NationalWeatherServiceData;
using Requests.NationalWeatherServiceData.Forecast;
using Requests.NationalWeatherServiceData.Points;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    public class NationalWeatherService
    {
        private readonly string baseEndpoint = $"https://api.weather.gov";
        public NationalWeatherService(string subscription_id, string participant_id, string device_id, string data_category, 
            string data_type, string agent_id = null, string definition_id = null, dynamic metadata = null)
        {
            this.subscription_id = subscription_id;
            this.participant_id = participant_id;
            this.device_id = device_id;
            this.data_category = data_category;
            this.data_type = data_type;
            this.agent_id = agent_id;
            this.definition_id = definition_id;
            this.metadata = metadata;
        }

        private string agent_id;
        private string data_category;
        private string data_type;
        private string definition_id;
        private string device_id;
        private dynamic metadata;
        private string timestamp_utc;
        private string subscription_id;
        private string partition_key;
        private string participant_id;

        public async Task GetSevenDayForecast(LatLongPoint location)
        {
            var forecastEndpoints = await GotForecastEndpoints(location);

            string endpoint = forecastEndpoints.forecastGridData;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            request.UserAgent = "SoCo Smart Neighborhoods";
            request.Accept = "application/ld+json";

            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    Console.WriteLine(val);
                    var response = JsonConvert.DeserializeObject<ForecastResponse>(val);

                    Messaging.WriteMessage("C:\\\\NWS", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task<NWSForecastEndpoints> GotForecastEndpoints(LatLongPoint location)
        {
            string endpoint = $"{baseEndpoint}/points/{location.Latitude},{location.Longitude}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            request.UserAgent = "SoCo Smart Neighborhoods";
            request.Accept = "application/ld+json";

            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    Console.WriteLine(val);
                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    var response = JsonConvert.DeserializeObject<PointsResponse>(val, settings);
                    return new NWSForecastEndpoints(response);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return new NWSForecastEndpoints(new PointsResponse());
        }

        private void HandleException(Exception ex)
        {
            if (ex.GetType().Name == "WebException")
            {
                var rsp = ((WebException)ex).Response;
                Stream outStr = rsp.GetResponseStream();
                long length = rsp.ContentLength;
                List<byte> outBytes = new List<byte>();

                int b = outStr.ReadByte();

                while (b != -1)
                {
                    outBytes.Add(Convert.ToByte(b));
                    b = outStr.ReadByte();
                }

                string val = Encoding.ASCII.GetString(outBytes.ToArray());

                Messaging.WriteMessage("C:\\\\NWS", "log.txt", $"Unabled to make request. {val} retured from server.{Environment.NewLine}", true);
            }
        }

        public async Task GetForecast(double[] location)
        {
            DateTime dtRequestDate = DateTime.UtcNow;
            UTF8Encoding encoding = new UTF8Encoding();
            double[] altusGeoLocation = new double[] { 33.8329279, -84.4229255 };
            int latitude = 0;
            int longitude = 1;

            string endpoint = $"{baseEndpoint}/points/{altusGeoLocation[latitude]},{altusGeoLocation[longitude]}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            request.UserAgent = "SoCo Smart Neighborhoods";
            request.Accept = "application/ld+json";
            request.Date = dtRequestDate;
            
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    NwsData nwsData = JsonConvert.DeserializeObject<NwsData>(val);
                    foreach (var item in nwsData.Graph)
                    {
                        if (item.sent <= DateTime.Now.AddDays(-7))
                        {
                            Messaging.WriteMessage("C:\\\\NWS", "log.txt", $"Found entry beyond seven days: {item}", true);
                        }
                    }
                    var resDocument = new RESDocument()
                    {
                        agent_id = agent_id,
                        data_category = data_category,
                        data_type = data_type,
                        definition_id = definition_id,
                        device_id = device_id,
                        envelope = nwsData,
                        metadata = metadata,
                        participant_id = participant_id,
                        partition_key = partition_key,
                        subscription_id = subscription_id,
                        timestamp_utc = timestamp_utc
                    };
                    Messaging.WriteMessage("C:\\\\NWS", "log.txt", $"Received:{Environment.NewLine}{JsonConvert.SerializeObject(resDocument, Formatting.Indented)}", true);
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "WebException")
                {
                    var rsp = ((WebException)ex).Response;
                    Stream outStr = rsp.GetResponseStream();
                    long length = rsp.ContentLength;
                    List<byte> outBytes = new List<byte>();

                    int b = outStr.ReadByte();

                    while (b != -1)
                    {
                        outBytes.Add(Convert.ToByte(b));
                        b = outStr.ReadByte();
                    }

                    string val = Encoding.ASCII.GetString(outBytes.ToArray());

                    Messaging.WriteMessage("C:\\\\NWS", "log.txt", $"Unabled to make request. {val} retured from server.{Environment.NewLine}", true);
                }
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
