using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    public class SensiboRequestor
    {
        private string APIKey = "W5OICSAYAPp2UXjetiS1uBwS8q7mkK";
        private string baseUrl = "https://home.sensibo.com/api/v2";
        private UTF8Encoding encoding = new UTF8Encoding();

        private static HttpWebRequest CreateSensiboRequest(string endpoint, DateTime dtRequestDate, string key, string method)
        {
            endpoint += $"apiKey={key}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = method;
            request.Date = dtRequestDate;
            return request;
        }

        public async Task GetDevices()
        {
            string endpoint = $"{baseUrl}/users/me/pods?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "GET");
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task GetDeviceTimer(string id)
        {
            string endpoint = $"{baseUrl}/pods/{id}/timer/?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "GET");
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task GetDeviceSmartmode(string id)
        {
            string endpoint = $"{baseUrl}/pods/{id}/smartmode?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "GET");
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task GetDeviceHistoricalSettings(string id)
        {
            string endpoint = $"{baseUrl}/pods/{id}/historicalMeasurements?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "GET");
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        internal async Task SetDeviceACState(string id, string property, string value)
        {
            string endpoint = $"{baseUrl}/pods/{id}/acStates/{property}?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "PATCH");
            try
            {
                // Turn AC On
                //string postData = "{ \"acState\": { \"on\": true, \"mode\": \"dry\", \"targetTemperature\": 72, \"temperatureUnit\": \"F\", \"swing\": \"stopped\" } }";
                // Turn AC Off
                string postData = "{ newValue: \"" + value + "\", \"acState\": { \"on\": false, \"mode\": \"dry\", \"targetTemperature\": 72, \"temperatureUnit\": \"F\", \"swing\": \"stopped\" } }";
                byte[] postDataBytes = encoding.GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                request.ContentType = "application/json";
                Stream outputStream = request.GetRequestStream();
                outputStream.Write(postDataBytes, 0, postDataBytes.Length);

                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }
        internal async Task SetDeviceACState(string id)
        {
            string endpoint = $"{baseUrl}/pods/{id}/acStates?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "POST");
            try
            {
                // Turn AC On
                //string postData = "{ \"acState\": { \"on\": true, \"mode\": \"dry\", \"targetTemperature\": 72, \"temperatureUnit\": \"F\", \"swing\": \"stopped\" } }";
                // Turn AC Off
                string postData = "{ \"acState\": { \"on\": false, \"mode\": \"dry\", \"targetTemperature\": 72, \"temperatureUnit\": \"F\", \"swing\": \"stopped\" } }";
                byte[] postDataBytes = encoding.GetBytes(postData);
                request.ContentLength = postDataBytes.Length;
                request.ContentType = "application/json";
                Stream outputStream = request.GetRequestStream();
                outputStream.Write(postDataBytes, 0, postDataBytes.Length);

                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task GetDevice(string id)
        {
            string endpoint = $"{baseUrl}/pods/{id}?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "GET");
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task GetDeviceACStates(string id)
        {
            string endpoint = $"{baseUrl}/pods/{id}/acStates?";
            DateTime dtRequestDate = DateTime.UtcNow;
            var request = CreateSensiboRequest(endpoint, dtRequestDate, APIKey, "GET");
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Received:{Environment.NewLine}{val}", true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        private async Task HandleException(Exception ex)
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

                Messaging.WriteMessage("C:\\\\Sensibo", "log.txt", $"Unabled to make request. {val} retured from server.{Environment.NewLine}", true);
            }
        }
    }
}
