namespace Taxually.TechnicalTest.Utility
{
    public class TaxuallyQueueClient : IDomainQueueClient
    {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload)
        {
            // Code to send to message queue removed for brevity
            return Task.CompletedTask;
        }
    }
}
