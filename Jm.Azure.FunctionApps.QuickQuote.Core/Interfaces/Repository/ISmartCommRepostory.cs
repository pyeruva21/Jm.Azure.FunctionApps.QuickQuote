

using System.Collections.Specialized;
using System.Net.Http;


namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    /// <summary>
    ///     Wrapper around the SmartComm service. Takes an incoming request verbatim and returns the response verbatim.
    /// </summary>
    public interface ISmartCommRepository
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Processes the given request against the specified <paramref name="path" /> on the SmartComm service using
        ///     the specified <paramref cref="method" /> and optional <paramref name="queryString" /> and
        ///     <paramref name="requestBody" />.
        /// </summary>
        /// <param name="method">
        ///     The HTTP method.
        /// </param>
        /// <param name="path">
        ///     The SmartComm path.
        /// </param>
        /// <param name="queryString">
        ///     The query string to append to the <paramref name="path" />.
        /// </param>
        /// <param name="queryStringParts">
        ///     The query string converted into a <see cref="NameValueCollection"/>.
        /// </param>
        /// <param name="requestBody">
        ///     The request body for POST requests.
        /// </param>
        /// <returns>
        ///     The response verbatim.
        /// </returns>
        ApiResult ProcessRequest(HttpMethod method, string path, string queryString = null, 
                NameValueCollection queryStringParts = null, string requestBody = null);

        #endregion
    }
}