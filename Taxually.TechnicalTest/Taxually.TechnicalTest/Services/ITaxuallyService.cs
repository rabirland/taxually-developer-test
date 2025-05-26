using Taxually.TechnicalTest.Domain;

namespace Taxually.TechnicalTest.Services;

public interface ITaxuallyService
{
    Task RegisterUKVatNumber(VatRegistrationRequest request);

    Task RegisterFrenchVatNumber(VatRegistrationRequest request);

    Task RegisterGermanVatNumber(VatRegistrationRequest request);
}
