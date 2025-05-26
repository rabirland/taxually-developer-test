namespace Taxually.TechnicalTest.Utility;

public interface IDomainQueueClient
{
    Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}
