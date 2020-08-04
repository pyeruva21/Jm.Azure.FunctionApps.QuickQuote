using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public interface IKeyVaultSettings
    {
        string OAuthConsumerSecret { get; }
        string OAuthSecretKey { get; }
    }
}
