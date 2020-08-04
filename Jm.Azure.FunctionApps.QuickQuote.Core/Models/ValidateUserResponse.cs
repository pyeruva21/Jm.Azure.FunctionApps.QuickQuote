using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public class ValidateUserResponse
    {
        public string Id { get; set; }
        public double Jsonrpc { get; set; }
        public bool Result { get; set; }
    }
}
