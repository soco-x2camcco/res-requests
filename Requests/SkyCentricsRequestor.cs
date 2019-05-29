using Newtonsoft.Json;
using Requests.SkyCentrics.Device;
using Requests.SkyCentrics.DeviceData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    public class SkyCentricsRequestor
    {
        private string utcDate;
        private UTF8Encoding encoding = new UTF8Encoding();

        public async Task GetDevices()
        {
            await MakeRequest();
        }

        // Suppress Async method lacks 'await' operators and will run synchronously
        // Using the Wait method instead of the await keyword and VS doesn't pick it up.
#pragma warning disable CS1998
        private async Task MakeRequest()
#pragma warning restore CS1998
        {
            string endpoint = "https://api.skycentrics.com/api/devices/";
            string clientId = "sVx2y6WJpNDfEjGgMCnA";
            string clientSecret = "wMnC7EgYje2PyTqNAsFdHjSxZtRWvBmNdAgKLwAp";
            string macAddress = "20f85ed88cf6";

            // Get Devices
            List<Device> devices = await RequestDevices(endpoint, clientId, clientSecret);
            string deviceId = devices[0].id.ToString();

            // Get Device Data
            endpoint = "https://api.skycentrics.com/api/devices/" + deviceId + "/data";
            DeviceData deviceData = await RequestDeviceData(endpoint, clientId, clientSecret, deviceId);

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task DeleteDevice(string mac)
        {
            string endpoint = "https://api.skycentrics.com/api/devices/";
            string clientId = "sVx2y6WJpNDfEjGgMCnA";
            string clientSecret = "wMnC7EgYje2PyTqNAsFdHjSxZtRWvBmNdAgKLwAp";
            DateTime dtRequestDate = DateTime.UtcNow;

            List<Device> devices = await RequestDevices(endpoint, clientId, clientSecret);
            string deviceId = devices.Find(d => d.mac == mac).id.ToString();

            endpoint = $"https://api.skycentrics.com/api/devices/{deviceId}/";

            string authHeader = "";

            var md5Hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes("");
            byte[] hash = md5Hash.ComputeHash(inputBytes);
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            byte[] keyBytes = encoding.GetBytes(clientSecret);
            HMACSHA1 sha1 = new HMACSHA1(keyBytes);

            authHeader = $"DELETE /api/devices/{deviceId}/ HTTP/1.1\n{dtRequestDate.ToString("r")}\n\n{sBuilder.ToString()}"; // Append MD5 Has on empty string
            byte[] authHeaderBytes = encoding.GetBytes(authHeader);
            byte[] shaHash = sha1.ComputeHash(authHeaderBytes);
            Console.WriteLine($"{Environment.NewLine}{authHeader}");
            authHeader = Convert.ToBase64String(shaHash).TrimEnd('\n');

            HttpWebRequest request = CreateSkyCentricsRequest(endpoint, dtRequestDate, $"{clientId}:{authHeader}", "DELETE");

#if (DEBUG)
            // This is used to enable debugging in Fiddler when running in Visual Studio
            request.Proxy = new WebProxy("127.0.0.1", 8888);
#endif

            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK || returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    devices = JsonConvert.DeserializeObject<List<Device>>(val);
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

                    Console.WriteLine($"Unabled to make request. {val} retured from server.{Environment.NewLine}");
                }
            }

        }

        /// <summary>
        /// Send a Set Event as a PUT
        /// </summary>
        /// <param name="device">Device ID (Note: This unique ID is an integer and is not the MAC Address)</param>
        /// <param name="eventName">The event name to send</param>
        public async Task SetEvent(string device, string eventName)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string endpoint = $"https://api.skycentrics.com/api/devices/{device}/event";
            string clientId = "sVx2y6WJpNDfEjGgMCnA";
            string clientSecret = "wMnC7EgYje2PyTqNAsFdHjSxZtRWvBmNdAgKLwAp";
            string macAddress = "20f85ed88cf6";
            DateTime dtRequestDate = DateTime.UtcNow;

            string authHeader = "";

            var md5Hash = MD5.Create();
            string strAuthRequest = $"{{ \"event\": \"{eventName}\", \"duration\": 1800 }}";
            byte[] data = md5Hash.ComputeHash(encoding.GetBytes(strAuthRequest));
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            byte[] putData = encoding.GetBytes(strAuthRequest);

            byte[] hash = md5Hash.ComputeHash(putData);

            byte[] keyBytes = encoding.GetBytes(clientSecret);
            HMACSHA1 sha1 = new HMACSHA1(keyBytes);

            authHeader = $"PUT /api/devices/{device}/event HTTP/1.1\n{dtRequestDate.ToString("r")}\n\n{sBuilder.ToString()}";
            byte[] authHeaderBytes = encoding.GetBytes(authHeader);
            byte[] shaHash = sha1.ComputeHash(authHeaderBytes);
            Console.WriteLine($"{Environment.NewLine}{authHeader}");
            authHeader = Convert.ToBase64String(shaHash).TrimEnd('\n');
            HttpWebRequest request = CreateSkyCentricsRequest(endpoint, dtRequestDate, $"{clientId}:{authHeader}", "PUT");

            try
            {
                Stream stream = await request.GetRequestStreamAsync();
                stream.Write(putData, 0, putData.Length);
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK || returnStatusCode == HttpStatusCode.Accepted)
                {

                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    Console.WriteLine($"Received: {JsonConvert.SerializeObject(val)}");
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

                    Console.WriteLine($"Unabled to make request. {val} retured from server.{Environment.NewLine}");
                }
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        private static HttpWebRequest CreateSkyCentricsRequest(string endpoint, DateTime dtRequestDate, string token, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = method;
            request.Date = dtRequestDate;
            request.Headers.Add("X-SC-API-TOKEN", token);
            return request;
        }

        public async Task SetLoadShed(string device, bool state)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string endpoint = $"https://api.skycentrics.com/api/devices/{device}/load_shed";
            string clientId = "sVx2y6WJpNDfEjGgMCnA";
            string clientSecret = "wMnC7EgYje2PyTqNAsFdHjSxZtRWvBmNdAgKLwAp";
            string macAddress = "20f85ed88cf6";
            DateTime dtRequestDate = DateTime.UtcNow;

            string authHeader = "";

            var md5Hash = MD5.Create();
            string stateValue = state ? "1" : "0";
            string strAuthRequest = $"{{ \"s\": {stateValue}, \"duration\": 120 }}";
            byte[] data = md5Hash.ComputeHash(encoding.GetBytes(strAuthRequest));
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            byte[] putData = encoding.GetBytes(strAuthRequest);

            byte[] hash = md5Hash.ComputeHash(putData);

            byte[] keyBytes = encoding.GetBytes(clientSecret);
            HMACSHA1 sha1 = new HMACSHA1(keyBytes);

            authHeader = $"PUT /api/devices/{device}/load_shed HTTP/1.1\n{dtRequestDate.ToString("r")}\n\n{sBuilder.ToString()}"; // Append MD5 Has on empty string
            byte[] authHeaderBytes = encoding.GetBytes(authHeader);
            byte[] shaHash = sha1.ComputeHash(authHeaderBytes);
            Console.WriteLine($"{Environment.NewLine}{authHeader}");
            authHeader = Convert.ToBase64String(shaHash).TrimEnd('\n');

            HttpWebRequest request = CreateSkyCentricsRequest(endpoint, dtRequestDate, $"{clientId}:{authHeader}", "PUT");

            try
            {
                Stream stream = await request.GetRequestStreamAsync();
                stream.Write(putData, 0, putData.Length);
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK || returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    Console.WriteLine($"Received: {JsonConvert.SerializeObject(val)}");
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

                    Console.WriteLine($"Unabled to make request. {val} retured from server.{Environment.NewLine}");
                }
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        public async Task SetDevice(Device device)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string endpoint = $"https://api.skycentrics.com/api/devices/{device.id}/";
            string clientId = "sVx2y6WJpNDfEjGgMCnA";
            string clientSecret = "wMnC7EgYje2PyTqNAsFdHjSxZtRWvBmNdAgKLwAp";
            string macAddress = "20f85ed88cf6";
            DateTime dtRequestDate = DateTime.UtcNow;

            string authHeader = "";

            var md5Hash = MD5.Create();
            string strAuthRequest = JsonConvert.SerializeObject(device);
            byte[] data = md5Hash.ComputeHash(encoding.GetBytes(strAuthRequest));
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            byte[] putData = encoding.GetBytes(strAuthRequest);

            byte[] hash = md5Hash.ComputeHash(putData);

            byte[] keyBytes = encoding.GetBytes(clientSecret);
            HMACSHA1 sha1 = new HMACSHA1(keyBytes);

            authHeader = $"PUT /api/devices/{device.id}/ HTTP/1.1\n{dtRequestDate.ToString("r")}\n\n{sBuilder.ToString()}"; // Append MD5 Has on empty string
            byte[] authHeaderBytes = encoding.GetBytes(authHeader);
            byte[] shaHash = sha1.ComputeHash(authHeaderBytes);
            Console.WriteLine($"{Environment.NewLine}{authHeader}");
            authHeader = Convert.ToBase64String(shaHash).TrimEnd('\n');

            HttpWebRequest request = CreateSkyCentricsRequest(endpoint, dtRequestDate, $"{clientId}:{authHeader}", "PUT");

            try
            {
                Stream stream = await request.GetRequestStreamAsync();
                stream.Write(putData, 0, putData.Length);
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK || returnStatusCode == HttpStatusCode.Accepted)
                {

                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    Console.WriteLine($"Received: {JsonConvert.SerializeObject(val)}");
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

                    Console.WriteLine($"Unabled to make request. {val} retured from server.{Environment.NewLine}");
                }
            }

            Console.WriteLine("Requests complete. Press any key to exit.");
            Console.ReadKey();
        }

        private async Task<List<Device>> RequestDevices(string endpoint, string clientId, string clientSecret)
        {
            DateTime dtRequestDate = DateTime.UtcNow;
            var devices = new List<Device>();

            string authHeader = "";

            var md5Hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes("");
            byte[] hash = md5Hash.ComputeHash(inputBytes);
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            byte[] keyBytes = encoding.GetBytes(clientSecret);
            HMACSHA1 sha1 = new HMACSHA1(keyBytes);

            authHeader = $"GET /api/devices/ HTTP/1.1\n{dtRequestDate.ToString("r")}\n\n{sBuilder.ToString()}"; // Append MD5 Has on empty string
            byte[] authHeaderBytes = encoding.GetBytes(authHeader);
            byte[] shaHash = sha1.ComputeHash(authHeaderBytes);
            Console.WriteLine($"{Environment.NewLine}{authHeader}");
            authHeader = Convert.ToBase64String(shaHash).TrimEnd('\n');

            HttpWebRequest request = CreateSkyCentricsRequest(endpoint, dtRequestDate, $"{clientId}:{authHeader}", "GET");

            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK || returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    devices = JsonConvert.DeserializeObject<List<Device>>(val);
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

                    Console.WriteLine($"Unabled to make request. {val} retured from server.{Environment.NewLine}");
                }
            }
            return devices;
        }

        private async Task<DeviceData> RequestDeviceData(string endpoint, string clientId, string clientSecret, string deviceId)
        {
            DeviceData deviceData = new DeviceData();
            DateTime dtRequestDate = DateTime.UtcNow;

            string authHeader = "";

            var md5Hash = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("");
            byte[] hash = md5Hash.ComputeHash(inputBytes);
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            byte[] keyBytes = encoding.GetBytes(clientSecret);
            HMACSHA1 sha1 = new HMACSHA1(keyBytes);

            authHeader = $"GET /api/devices/{deviceId}/data HTTP/1.1\n{dtRequestDate.ToString("r")}\n\n{sBuilder.ToString()}"; // Append MD5 Has on empty string
            byte[] authHeaderBytes = encoding.GetBytes(authHeader);
            byte[] shaHash = sha1.ComputeHash(authHeaderBytes);
            Console.WriteLine($"{Environment.NewLine}{authHeader}");
            authHeader = Convert.ToBase64String(shaHash).TrimEnd('\n');

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            request.Date = dtRequestDate;
            request.Headers.Add("X-SC-API-Token", $"{clientId}:{authHeader}");

            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK || returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    deviceData = JsonConvert.DeserializeObject<DeviceData>(val);
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

                    Console.WriteLine($"Unabled to make request. {val} retured from server.{Environment.NewLine}");
                }
            }
            return deviceData;
        }

        private static string GenerateSkyCentricsSignature(string verb, string requestUri, string httpVersion, string contentType, string utcDate, string clientId, string clientSecret)
        {
            var hmacSha1 = new HMACSHA1 { Key = Convert.FromBase64String(clientSecret) };
            string payLoad = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} {1} {2}\n{3}\n{4}\n",
                    verb,
                    requestUri,
                    httpVersion,
                    utcDate,
                    contentType
            );

            byte[] hashPayLoad = hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(payLoad));
            string signature = Convert.ToBase64String(hashPayLoad);

            var result = String.Format(
                    System.Globalization.CultureInfo.InvariantCulture, "{0}:{1}",
                    clientId,signature
                );
            Console.WriteLine($"Generating Token for Payload as:{Environment.NewLine}{payLoad}{Environment.NewLine}Signature is: {signature}.{Environment.NewLine}Result: {result}{Environment.NewLine}");

            return result;
        }
    }

    class SkyCentricsAuthDocument
    {
        public int id { get; set; }
        public int user { get; set; }
        public int group { get; set; }
        public int type { get; set; }
        public string mac { get; set; }
        public string name { get; set; }
    }
}
