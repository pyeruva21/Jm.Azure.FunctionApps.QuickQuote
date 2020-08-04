using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Azure.FunctionApps.QuickQuote.Integration.Models
{
    public class ValidateUserResponse
    {
        public string Id { get; set; }
        public double Jsonrpc { get; set; }
        public string Method { get; set; }
    }
}
