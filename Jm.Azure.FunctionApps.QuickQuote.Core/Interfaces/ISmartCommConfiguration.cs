// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISmartCommConfiguration.cs" company="Jewelers Mutual Insurance Corporation">
//   Copyright © Jewelers Mutual Insurance Corporation
// </copyright>
// <summary>
//   Defines the ISmartCommConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    /// <summary>
    ///     Contains configuration values for communicating with the SmartComm service.
    /// </summary>
    public interface ISmartCommConfiguration
    {
        #region Properties

        /// <summary>
        ///     Gets the OAuth consumer secret.
        /// </summary>
        /// <value>
        ///     The OAuth consumer secret.
        /// </value>
        string OAuthConsumerSecret { get; }

        /// <summary>
        ///     Gets the OAuth token secret.
        /// </summary>
        /// <value>
        ///     The OAuth token secret.
        /// </value>
        string OAuthTokenSecret { get; }

        /// <summary>
        ///     Gets the OAuth URL.
        /// </summary>
        /// <value>
        ///     The OAuth URL.
        /// </value>
        string OAuthUrl { get; }

        /// <summary>
        ///     Gets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        string UserId { get; }

        /// <summary>
        /// Gets the batch configuration resource identifier.
        /// </summary>
        /// <value>
        /// The batch configuration resource identifier.
        /// </value>
        int BatchConfigResId { get; }

        /// <summary>
        /// Gets the job queue name.
        /// </summary>
        /// <value>
        /// The job queue name.
        /// </value>
        string JobQueue { get; }

        /// <summary>
        /// Gets the URL of the Activity Logger API.
        /// </summary>
        /// <value>
        /// The URL of the Activity Logger API.
        /// </value>
        string ActivityLoggerApiUrl { get; }

        #endregion
    }
}