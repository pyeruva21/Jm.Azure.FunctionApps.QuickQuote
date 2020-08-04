namespace Jm.Azure.FunctionApps.QuickQuote.Core
{
    public interface IAppSettings
    {
        string UserId { get; }
        string OAuthUrl { get; }
        string JobQueue { get; }
        string ActivityLoggerApiUrl { get; }        
    }
}
