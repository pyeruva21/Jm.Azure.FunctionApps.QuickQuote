using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jm.Azure.FunctionApps.QuickQuote.Integration.Bases
{
    public class GuidewireEdgeRpcRepositoryBase
    {
        private const string TokenPrefix = "Bearer";

        static readonly HttpClient client = new HttpClient();

        protected async Task<T> Post<T>(string token, string serviceUrl, string methodName)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, serviceUrl);

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(TokenPrefix, token);

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseContent);
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var exceptionMessage = $"Call to {serviceUrl} method {methodName} failed with response {responseContent}.";
                throw new HttpRequestException(exceptionMessage);
            }

        }
    }
}
