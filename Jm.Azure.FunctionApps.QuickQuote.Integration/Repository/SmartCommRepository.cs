
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Jm.Azure.FunctionApps.QuickQuote.Core;
using Microsoft.AspNetCore.Authentication;
using RestSharp;

namespace Jm.Azure.FunctionApps.QuickQuote.Intergration
{
    /// <summary>
    ///     Wrapper around the SmartComm service. Takes an incoming request verbatim and returns the response verbatim.
    /// </summary>
    public class SmartCommRepository : ISmartCommRepository
    {
        #region Fields

        //private readonly IClock _clock;

        private readonly ISmartCommConfiguration _configuration;

        //private readonly IActivityLogger _logger;

        private readonly IRestClient _restClient;
        private readonly ISystemClock _clock;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmartCommService" /> class.
        /// </summary>
        /// <param name="restClient">
        ///     The rest client.
        /// </param>
        /// <param name="configuration">
        ///     The SmartComm configuration.
        /// </param>
        /// <param name="logger">
        ///     The logger.
        /// </param>
        /// <param name="clock">The clock.</param>
        public SmartCommRepository(IRestClient restClient, ISmartCommConfiguration configuration,
            ISystemClock clock)
        {
            this._restClient = restClient;
            this._configuration = configuration;
            //this._logger = logger;
            this._clock = clock;
        }

        #endregion

        #region Public Methods and Operators
        
        /// <inheritdoc />
        public ApiResult ProcessRequest(HttpMethod method, string path, string queryString = null, 
            NameValueCollection queryStringParts = null, string requestBody = null)
        {
            var result = new ApiResult();

            try
            {
                // Build the SmartComm URL by using the passed in path and tacking on any query string parameters that were passed into our API.
                //var absoluteUrl = $"{this._configuration.OAuthUrl}";
                //var fullUri = new Uri($"{absoluteUrl}");

                var absoluteUrl = $"{this._configuration.OAuthUrl}{path}";
                var fullUri = new Uri($"{absoluteUrl}{queryString}");

                _restClient.BaseUrl = new Uri(this._configuration.OAuthUrl);
                //_restClient.AddDefaultHeader("Content-Type", $"application/xml");

                /* 
                 * Steps for generating a valid OAuth authorization header as outlined here: https://dev.twitter.com/oauth/overview/creating-signatures
                 *   1) Build Sorted (alpha) Dictionary of all query string parameters key/value along with standard OAuth key values.
                 *   2) Encode the parameters and concatenate items from STEP 1, delimited with ampersands.
                 *   3) Build array used for creating the Signature Base String, contains 3 parts: HTTP Verb, URL, and encoded string from STEP 2.
                 *   4) Concatenate items from STEP 3, delimited with ampersands.
                 *   5) Generate Signing Key: in our case, it is just the OAuthTokenSecret followed by an ampersand.
                 *   6) Calculate Signature. Use HMAC-SHA1 algorithm and hash the value from STEP 4 using the key from STEP 5. Finally Base64 encode the result.
                 *   7) Add the result from STEP 6 to the original Dictionary in STEP 1.
                 *   8) Concatenate (again) the Dictionary from STEP 1 (this time including the Signature), delimited this time with commas.
                 *   9) Add the resulting string value to the HTTP Headers, Key = Authorization, Value = "OAuth {Value from STEP 8}"
                 */
                var nonce = Guid.NewGuid().ToString();
                var timeStamp = this._clock.UtcNow.Ticks.ToString();

                // STEP 1
                var oauthParametersParts = new SortedDictionary<string, string>();
                oauthParametersParts.Add("oauth_consumer_key", $"{this._configuration.OAuthConsumerSecret}%21{Uri.EscapeDataString(this._configuration.UserId)}");
                oauthParametersParts.Add("oauth_nonce", nonce);
                oauthParametersParts.Add("oauth_signature_method", "HMAC-SHA1");
                oauthParametersParts.Add("oauth_timestamp", timeStamp);
                oauthParametersParts.Add("oauth_version", "1.0");

                queryStringParts = queryStringParts ?? new NameValueCollection();
                foreach (var key in queryStringParts.AllKeys)
                {
                    oauthParametersParts.Add(key, Uri.EscapeDataString(queryStringParts.Get(key)));
                }

                // STEP 2
                var oauthParametersString = string.Join("&", oauthParametersParts.Select(x => $"{x.Key}={x.Value}"));

                // STEP 3
                var oauthSignatureBaseParts = new[] { method.ToString(), Uri.EscapeDataString(absoluteUrl), Uri.EscapeDataString(oauthParametersString) };

                // STEP 4
                var oauthSignatureBaseString = string.Join("&", oauthSignatureBaseParts);

                // STEP 5
                var signingKey = $"{this._configuration.OAuthTokenSecret}&";

                // STEP 6
                var signature = Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(signingKey)).ComputeHash(Encoding.UTF8.GetBytes(oauthSignatureBaseString)));

                // STEP 7
                oauthParametersParts.Add("oauth_signature", Uri.EscapeDataString(signature));

                // STEP 8
                var headerValue = string.Join(",", oauthParametersParts.Select(x => $"{x.Key}={x.Value}"));

                // STEP 9
                //var options = new RestOptions { RequestContentType = "application/xml", 
                //    ResponseAcceptType = "application/xml", RequestBody = requestBody };
                //options.AdditionalHeaders.Add(new KeyValuePair<string, string>("Authorization", $"OAuth {headerValue}"));

                // Keeping this only because it is useful for new job debugging
                //this._logger.LogActivity(
                //    this._configuration.ActivityLoggerApiUrl, 
                //    "DocumentGenerator",
                //    $"Calling SmartComm with this URL: {fullUri}; Request Payload: {requestBody}", 
                //    "Request");

                RestRequest request = new RestRequest(path, Method.POST);
                request.AddHeader("Content-Type", $"application/xml");
                request.AddParameter("templateId", "158046355", ParameterType.QueryString);
                //request.AddHeader("Accept-Type", $"application/xml");
                request.AddHeader("Authorization", $"OAuth {headerValue}");
                var response = this._restClient.ExecuteAsPost(request, "POST");

                if (response.StatusCode != HttpStatusCode.OK )//&& response.StatusCode != HttpStatusCode.Created)
                {
                    //this._logger.LogActivity(
                    //this._configuration.ActivityLoggerApiUrl,
                    //"DocumentGenerator",
                    //$"SmartComm Response Code: {result.StatusCode}; Content: {result.Content}; URL:{fullUri}",
                    //"Bad Response");
                }
            }
            catch (Exception ex)
            {
                //this._logger.LogActivity(
                //    this._configuration.ActivityLoggerApiUrl,
                //    "DocumentGenerator",
                //    $"Caught Exception: {ex.Message}",
                //    "Error");

                //result.AddError(ApiErrorType.Error, "DCC2A913-7061-40DB-81B2-5E200F4521DB", ex.Message, ex.ToString());
            }

            return result;
        }

        #endregion
    }
}