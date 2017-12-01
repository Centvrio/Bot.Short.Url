namespace Centvrio.Bot.Short.Url.Providers.Results
{
    public interface IFaultyResult : IHttpResult
    {
        bool Failed { get; }

        string FailMessage { get; }
    }
}
