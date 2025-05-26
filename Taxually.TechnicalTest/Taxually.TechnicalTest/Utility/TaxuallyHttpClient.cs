namespace Taxually.TechnicalTest.Utility
{
    public class TaxuallyHttpClient : IDomainHttpClient
    {
        public Task PostAsync<TRequest>(string url, TRequest request)
        {
            // Actual HTTP call removed for purposes of this exercise
            return Task.CompletedTask;
        }
    }
}
