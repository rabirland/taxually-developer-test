namespace Taxually.TechnicalTest.Domain;

public class VatRegistrationRequest
{
    public string CompanyName { get; set; }
    public string CompanyId { get; set; }
    public string Country { get; set; }

    public VatRegistrationRequest(string companyName, string companyId, string country)
    {
        CompanyName = companyName;
        CompanyId = companyId;
        Country = country;
    }
}