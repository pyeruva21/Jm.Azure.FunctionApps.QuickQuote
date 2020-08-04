using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Azure.FunctionApps.QuickQuote.Functions
{
    public class QuickQuoteRequest
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
    }
}
