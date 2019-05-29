using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests.RESi
{
    public class RequestParticipants
    {
        private string baseEndpoint = "http://localhost:14330";        

        public async Task GetParticipants(string sid, string subscriptionid, string acctNum, bool activeOnly)
        {
            DateTime dtRequestDate = DateTime.UtcNow;
            UTF8Encoding encoding = new UTF8Encoding();

            string path = "/api/Participant/GetParticipant";
            string endpoint = $"{baseEndpoint}/{path}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            request.Date = dtRequestDate;
            request.Accept = "text/plain";
            request.Host = "localhost:14330";

            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept-Language", "en-US");

            request.Headers.Add("sid", sid);
            request.Headers.Add("subscriptionId", subscriptionid);
            request.Headers.Add("acctNum", acctNum);
            request.Headers.Add("activeOnly", activeOnly.ToString());

            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();

                    Messaging.WriteMessage("C:\\\\RESi", "log.txt", $"Received:{Environment.NewLine}{val}", true);
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

                    Messaging.WriteMessage("C:\\\\RESi", "log.txt", $"Unabled to make request. {val} retured from server.{Environment.NewLine}", true);
                }
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
