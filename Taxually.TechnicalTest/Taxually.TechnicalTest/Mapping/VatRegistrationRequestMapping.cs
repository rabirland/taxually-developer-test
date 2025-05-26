using System.Xml.Serialization;
using Taxually.TechnicalTest.Domain;
using Taxually.TechnicalTest.Dto;

namespace Taxually.TechnicalTest.Mapping;

public static class VatRegistrationRequestMapping
{
    public static VatRegistrationRequest ToDomain(this VatRegistrationRequestDto dto)
    {
        return new VatRegistrationRequest(
            dto.CompanyName,
            dto.CompanyId,
            dto.Country);
    }

    public static string ToFrenchRequestCsv(this VatRegistrationRequest domain)
    {
        return $"CompanyName,CompanyId{Environment.NewLine}{domain.CompanyName},{domain.CompanyId}";
    }

    public static string ToGermanRequestXml(this VatRegistrationRequest domain)
    {
        using var stringWriter = new StringWriter();

        var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
        serializer.Serialize(stringWriter, domain);
        return stringWriter.ToString();
    }
}
