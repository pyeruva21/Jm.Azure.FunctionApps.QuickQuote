using Jm.Azure.FunctionApps.QuickQuote.Core;

namespace Jm.Azure.FunctionApps.QuickQuote.Functions
{
    /// <summary>
    ///     The <see cref="ISmartCommConfiguration" /> implementation that reads values from the web.config.
    /// </summary>
    public class SmartCommFileConfiguration : ISmartCommConfiguration
    {
        #region Constructors
        /// <summary>
        ///     Initializes a new instance of the <see cref="SmartCommFileConfiguration" /> class.
        /// </summary>
        public SmartCommFileConfiguration(IAppSettings appSettings, IKeyVaultSettings keyVaultSettings)
        {
            this.OAuthConsumerSecret = keyVaultSettings.OAuthConsumerSecret;// this.GetAppSetting("SmartCommConsumerKey", string.Empty);
            this.OAuthTokenSecret = keyVaultSettings.OAuthSecretKey;// this.GetAppSetting("SmartCommSecretKey", string.Empty);
            this.UserId = appSettings.UserId;// this.GetAppSetting("SmartCommUserID", string.Empty);
            this.OAuthUrl = appSettings.OAuthUrl;// this.GetAppSetting("SmartCommRestURL", string.Empty);
            //this.JobQueue = appSettings.JobQueue;// this.GetAppSetting("SmartCommJobQueue", string.Empty);
            //this.ActivityLoggerApiUrl = appSettings.ActivityLoggerApiUrl;// this.GetAppSetting("ActivityLoggerApiUrl", string.Empty);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public string OAuthConsumerSecret { get; }

        /// <inheritdoc />
        public string OAuthTokenSecret { get; }

        /// <inheritdoc />
        public string OAuthUrl { get; }

        /// <inheritdoc />
        public string UserId { get; }

        /// <inheritdoc />
        public int BatchConfigResId { get; }

        /// <inheritdoc />
        public string JobQueue { get; }

        /// <inheritdoc />
        public string ActivityLoggerApiUrl { get; }

        #endregion
    }
}