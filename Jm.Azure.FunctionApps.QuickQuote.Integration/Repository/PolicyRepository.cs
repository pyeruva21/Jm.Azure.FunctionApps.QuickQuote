using AutoMapper;
using Jm.Azure.FunctionApps.QuickQuote.Core;
using Jm.Azure.FunctionApps.QuickQuote.Integration.Bases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jm.Azure.FunctionApps.QuickQuote.Integration.Repository
{
    public class PolicyRepository : GuidewireEdgeRpcRepositoryBase, IPolicyRepository
    {
        private readonly IMapper _mapper;
        static readonly HttpClient _client = new HttpClient();
        public PolicyRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ValidateUserResponse> ValidateUser(string token, string userId)
        {
            var endpoint = $"{Environment.GetEnvironmentVariable("ApimEndpoint")}/validateuser";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            request.Headers.Add("Ocp-Apim-Subscription-Key", 
                        Environment.GetEnvironmentVariable("ApimQuickQuoteSubcriptionKey"));
            request.Headers.Add("Authorization", $"Bearer {token}");


            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();

                throw new Exception($"{endpoint} returned a non-successful status code. Code was {response.StatusCode} with reason {response.ReasonPhrase}. Body was {errorResponse}.");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<ValidateUserResponse>(responseString);

            var searchResultModel = _mapper.Map<ValidateUserResponse>(searchResult);

            return searchResultModel;
        }
    }
}
