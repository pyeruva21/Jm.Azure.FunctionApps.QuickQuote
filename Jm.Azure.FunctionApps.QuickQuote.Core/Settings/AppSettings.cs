using System;
namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public class AppSettings : IAppSettings
    {
        public string UserId
        {
            get
            {
                var key = AppSettingsKeys.UserId;

                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception($"No environment setting could be found with key {key}.");
                }

                return Environment.GetEnvironmentVariable(key);
            }
        }

        public string OAuthUrl
        {
            get
            {
                var key = AppSettingsKeys.OAuthUrl;

                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception($"No environment setting could be found with key {key}.");
                }

                return Environment.GetEnvironmentVariable(key);
            }
        }


        public string JobQueue
        {
            get
            {
                var key = AppSettingsKeys.JobQueue;

                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception($"No environment setting could be found with key {key}.");
                }

                return Environment.GetEnvironmentVariable(key);
            }
        }

        public string ActivityLoggerApiUrl
        {
            get
            {
                var key = AppSettingsKeys.ActivityLoggerApiUrl;

                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception($"No environment setting could be found with key {key}.");
                }

                return Environment.GetEnvironmentVariable(key);
            }
        }
    }
}
