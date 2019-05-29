using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    internal class ApiManagementRequestor
    {
        string subscriptionId = "b55c6b59-6dfe-42e7-8a60-4bd9a3736937";
        string serviceName = "socoresapi";
        string resourceGroup = "SouthernCompany.RES.API";
        string apiVersion = "2018-06-01-preview";
        string baseUrl = "";

        public ApiManagementRequestor()
        {
            baseUrl = string.Format("https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.ApiManagement/service/{2}/", subscriptionId, resourceGroup, serviceName);
        }

        internal async Task ListBackend()
        {
            string endpoint = baseUrl + "backends?api-version=2018-06-01-preview";
            //string authHeader = GenerateMasterKeyAuthorizationSignature("GET", );

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            //request.Headers.Add("authorization", authHeader);

        }

        private string GenerateMasterKeyAuthorizationSignature(string verb, string resourceId, string resourceType, string key, string keyType, string tokenVersion, string utcDate)
        {
            var hmacSha256 = new System.Security.Cryptography.HMACSHA256 { Key = Convert.FromBase64String(key) };

            string payLoad = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n",
                    verb.ToLowerInvariant(),
                    resourceType.ToLowerInvariant(),
                    resourceId,
                    utcDate.ToLowerInvariant(),
                    ""
            );

            byte[] hashPayLoad = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(payLoad));
            string signature = Convert.ToBase64String(hashPayLoad);

            return System.Web.HttpUtility.UrlEncode(String.Format(System.Globalization.CultureInfo.InvariantCulture, "type={0}&ver={1}&sig={2}",
                keyType,
                tokenVersion,
                signature));
        }
    }
}