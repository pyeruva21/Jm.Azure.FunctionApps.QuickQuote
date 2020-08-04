using Jm.Azure.FunctionApps.QuickQuote.Core.Static;
using Microsoft.Extensions.Configuration;
using System;

namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public class KeyVaultSettings : IKeyVaultSettings
    {
        private readonly IConfiguration _configuration;

        public KeyVaultSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string OAuthConsumerSecret
        {
            get
            {
                var username = KeyVaultKeys.OAuthConsumerSecret;

                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception($"No secret could be found in KeyVault with key {KeyVaultKeys.OAuthConsumerSecret}.");
                }

                return _configuration[username];
            }
        }

        public string OAuthSecretKey
        {
            get
            {
                var username = KeyVaultKeys.OAuthSecretKey;

                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception($"No secret could be found in KeyVault with key {KeyVaultKeys.OAuthSecretKey}.");
                }

                return _configuration[username];
            }
        }


    }
}
