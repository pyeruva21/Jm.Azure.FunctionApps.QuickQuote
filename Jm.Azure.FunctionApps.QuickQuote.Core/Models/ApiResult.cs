using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public class ApiResult
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
}
