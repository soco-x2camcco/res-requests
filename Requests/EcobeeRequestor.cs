using Newtonsoft.Json;
using Requests.Ecobee;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    public class EcobeeRequestor
    {
        private string utcDate;
        private UTF8Encoding encoding = new UTF8Encoding();
        private string apiKey = "YbMXLzEXoZtLtdVWpwujVaB3Vzbiz0dw";

        public async Task GetDevices()
        {
            await MakeRequest();
        }
        private string deviceId = "0";

        private string baseUrl = "https://api.ecobee.com";
        // Suppress Async method lacks 'await' operators and will run synchronously
        // Using the Wait method instead of the await keyword and VS doesn't pick it up.
#pragma warning disable CS1998
        private async Task MakeRequest()
#pragma warning restore CS1998
        {
            string responseType = "code";
            string clientId = apiKey; // We'll have to get an Utility account for this
            string returnUri = "rescua.southerncompany.com"; 
            string requestState = "48DC9609-4F4F-426A-A6B2-4F8C1E924921";
            string authEndpoint = $"{baseUrl}/authorize?response_type={responseType}&client_id={clientId}&redirect_uri={returnUri}&scope=smartWrite&state={requestState}";

            //var tokenResponse = await RefreshToken(requestState);

            string token = GetAccessToken(requestState);

            await GetThermostats(token);
        }


        private string GetAccessToken(string participantId)
        {
            string accessToken = string.Empty;
            string connectionString = "Server=socoresdb-ua.database.windows.net;Database=resdb_ua;User ID=ServerAdmin;Password=F1r3ball;";
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT ost.access_token FROM[dbo].[oauth_session_token] ost join oauth_session os on ost.session_pk_id = os.id join market_participant mp on os.participant_pk_id = mp.id where mp.participant_id = @participantId";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@participantId", participantId.ToString());
                connection.Open();
                accessToken = command.ExecuteScalar().ToString();
            }
            return accessToken;
        }

        private async Task<TokenResponse> RefreshToken(string participantId)
        {
            string refreshToken;
            string connectionString = "Server=socoresdb-ua.database.windows.net;Database=resdb_ua;User ID=ServerAdmin;Password=F1r3ball;";
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT ost.refresh_token FROM[dbo].[oauth_session_token] ost join oauth_session os on ost.session_pk_id = os.id join market_participant mp on os.participant_pk_id = mp.id where mp.participant_id = @participantId";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@participantId", participantId.ToString());
                connection.Open();
                refreshToken = command.ExecuteScalar().ToString();
            }
            string endpoint = $"{baseUrl}/token?grant_type=refresh_token&code={refreshToken}&client_id={apiKey}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.ContentType = "text/json";
            request.Method = "POST";
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<TokenResponse>(val);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return null;
        }

        private async Task GetThermostats(string token)
        {
            string endpoint = $"{baseUrl}/1/thermostat?json={{\"selection\":{{\"includeAlerts\":\"true\",\"selectionType\":\"registered\",\"selectionMatch\":\"\",\"includeEvents\":\"true\",\"includeSettings\":\"true\",\"includeRuntime\":\"true\"}} }}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.ContentType = "text/json";
            request.Method = "GET";
            try
            {
                HttpWebResponse rsp = await request.GetResponseAsync() as HttpWebResponse;

                HttpStatusCode returnStatusCode = rsp.StatusCode;

                if (returnStatusCode == HttpStatusCode.OK ||
                    returnStatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader sr = new StreamReader(rsp.GetResponseStream());
                    string val = sr.ReadToEnd();
                    var response = JsonConvert.DeserializeObject<ThermostatListResponse>(val);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
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

                Messaging.WriteMessage("C:\\\\Ecobee", "log.txt", $"Unabled to make request. {val} retured from server.{Environment.NewLine}", true);
            }
        }
    }

    internal class TokenResponse
    {
        public string access_token;
        public string token_type;
        public int expires_in;
        public string refresh_token;
        public string scope;
    }
}
