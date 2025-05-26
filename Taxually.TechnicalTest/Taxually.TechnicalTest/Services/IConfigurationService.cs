namespace Taxually.TechnicalTest.Services;

public interface IConfigurationService
{
    public string UkVatRegistrationEndpoint { get; }

    public string GermanVatRegistrationQueueName { get; }

    public string FrenchVatRegistrationQueueName { get; }
}
