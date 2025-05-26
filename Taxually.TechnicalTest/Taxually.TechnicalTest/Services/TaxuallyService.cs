using System.Text;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Domain;
using Taxually.TechnicalTest.Mapping;
using Taxually.TechnicalTest.Utility;

namespace Taxually.TechnicalTest.Services;

public class TaxuallyService : ITaxuallyService
{
    private readonly IDomainHttpClient _httpClient;
    private readonly IDomainQueueClient _queueClient;
    private readonly IConfigurationService _configurationService;

    public TaxuallyService(IDomainHttpClient httpClient, IDomainQueueClient queueClient, IConfigurationService configurationService)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        // NOTE: Below there was a "excelQueueClient" and another "xmlQueueClient".
        // If they are two different implementations, they could be separated by
        // either a different interface or using keyed-services.
        _queueClient = queueClient ?? throw new ArgumentNullException(nameof(queueClient));
        _configurationService = configurationService;
    }

    public Task RegisterUKVatNumber(VatRegistrationRequest request)
    {
        if (request.Country != "GB")
        {
            throw new ArgumentException("Invalid country code for UK VAT registration", nameof(request));
        }

        // NOTE: We could await here for a bit cleaner code, but not doing so
        // does not create another task state machine
        // for a cleaner separation, arguably there could be another DTO specific for the UK registration endpoint
        return _httpClient.PostAsync(_configurationService.UkVatRegistrationEndpoint, request);
    }

    public Task RegisterFrenchVatNumber(VatRegistrationRequest request)
    {
        if (request.Country != "FR")
        {
            throw new ArgumentException("Invalid country code for French VAT registration", nameof(request));
        }

        // NOTE: It was missing the delimiter
        // Also does not properly escape the values
        // Having StringBuilder for one single value seemed to be an overkill
        var csvString = request.ToFrenchRequestCsv();
        var csv = Encoding.UTF8.GetBytes(csvString);
        // NOTE: In the UK version, we send the string, instead of a byte array.
        // without further domain knowledge, I left it as is, but probably we could also do the same here.
        return _queueClient.EnqueueAsync(_configurationService.FrenchVatRegistrationQueueName, csv);
    }

    public Task RegisterGermanVatNumber(VatRegistrationRequest request)
    {
        if (request.Country != "DE")
        {
            throw new ArgumentException("Invalid country code for German VAT registration", nameof(request));
        }

        // NOTE: Was using the service (originally the controller) as the to-serialize object
        var xmlString = request.ToGermanRequestXml();

        return _queueClient.EnqueueAsync(_configurationService.GermanVatRegistrationQueueName, xmlString);
    }
}
