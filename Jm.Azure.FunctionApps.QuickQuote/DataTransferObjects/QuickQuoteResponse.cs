using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jm.Azure.FunctionApps.QuickQuote.Functions.DataTransferObjects
{
    public class QuickQuoteResponse
    {
        [JsonProperty(PropertyName = "result")]
        public QuickQuotePdfResult Result{ get; set; }

        [JsonProperty(PropertyName = "jsonrpc")]
        public double JsonRpc { get; set; }
    }
}
