namespace Taxually.TechnicalTest.Services;

public class ConstantConfigurationService : IConfigurationService
{
    public string UkVatRegistrationEndpoint => "https://api.uktax.gov.uk";

    public string FrenchVatRegistrationQueueName => "vat-registration-csv";

    public string GermanVatRegistrationQueueName => "vat-registration-xml";
}
