using Newtonsoft.Json;
using Requests.DeltaInverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    class DeltaInverterRequestor
    {
        private string baseUrl = "https://delta-solar.vidagrid.com/";
        private string utcDate;
        private UTF8Encoding encoding = new UTF8Encoding();
        private string password = "Southernco";
        private string email = "G2RESGPCALAPI@southernco.com";

        public async Task<string> GetHistory()
        {
            string endpoint = $"{baseUrl}api/history"; HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            string auth = GetAuthHeader();
            request.Headers.Add("Authorization", auth);
            request.ContentType = "text/json";
            request.Method = "GET";
            string response = "";
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    response = val;
                    //response = JsonConvert.DeserializeObject<BindResponse>(val);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return response;

        }

        public async Task<BindResponse> Bind(string sn)
        {
            string endpoint = $"{baseUrl}api/bind/{sn}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            string auth = GetAuthHeader();
            request.Headers.Add("Authorization", auth);
            request.Method = "POST";
            BindResponse response = null;
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    response = JsonConvert.DeserializeObject<BindResponse>(val);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return response;
        }

        public string GetAuthHeader()
        {
            return $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{password}"))}";
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

                Messaging.WriteMessage("C:\\\\Delta", "log.txt", $"Unabled to make request. {val} retured from server.{Environment.NewLine}", true);
            }
        }
    }
}

