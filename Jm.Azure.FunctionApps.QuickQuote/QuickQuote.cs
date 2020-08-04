using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AutoMapper;
using Jm.Azure.FunctionApps.QuickQuote.Core;
using System.Net.Http;
using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;
using Jm.Azure.FunctionApps.QuickQuote.Functions;
using Jm.Azure.FunctionApps.QuickQuote.Functions.DataTransferObjects;

namespace Jm.Azure.FunctionApps.QuickQuote
{
    public class QuickQuote
    {
        private readonly ISmartCommRepository _smartCommRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;
        private readonly string _path = "/api/v7/job/generateDocument";
        private const string HeaderName = "Authorization";
        private const string TokenPrefix = "Bearer ";

        public QuickQuote(
            ISmartCommRepository smartCommRepository,
            IPolicyRepository policyRepository,
            IMapper mapper
            )
        {
            _smartCommRepository = smartCommRepository;
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        [FunctionName("QuickQuotePrintPdf")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<QuickQuoteRequest>(requestBody);
            string bearerToken;
            if (req.Headers.TryGetValue(HeaderName, out StringValues headersOut))
            {
                if (headersOut.ToString().Length <= TokenPrefix.Length)
                    return new UnauthorizedResult();

                bearerToken = headersOut.ToString().Substring(TokenPrefix.Length);

                if (string.IsNullOrEmpty(bearerToken))
                    return new UnauthorizedResult();
            }
            else
                return new UnauthorizedResult();

            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var validationResponse = await _policyRepository.ValidateUser(bearerToken, request.Id);

            //var queryStringParts = new NameValueCollection { { "templateId", "158046355" } };

            //var result = _smartCommRepository.ProcessRequest(HttpMethod.Post, _path, req.QueryString.Value,
            //            queryStringParts, null);

            //var quickQuoteDto = _mapper.Map<QuickQuoteResponse>(result);
            return new OkObjectResult(validationResponse);
        }
    }
}
