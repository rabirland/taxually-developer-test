namespace Taxually.TechnicalTest.Utility;

public interface IDomainHttpClient
{
    Task PostAsync<TRequest>(string url, TRequest request);
}
