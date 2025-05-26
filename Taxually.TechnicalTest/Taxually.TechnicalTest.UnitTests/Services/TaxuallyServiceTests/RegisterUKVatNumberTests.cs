using FluentAssertions;
using Moq;
using Taxually.TechnicalTest.Domain;
using Taxually.TechnicalTest.Services;
using Taxually.TechnicalTest.Utility;

namespace Taxually.TechnicalTest.UnitTests.Services.TaxuallyServiceTests;

[TestClass]
public class RegisterUKVatNumberTests
{
    [TestMethod]
    public void ShouldAcceptUKCountryCode()
    {
        // Arragnge
        var httpClient = new Mock<IDomainHttpClient>();
        var queueClient = new Mock<IDomainQueueClient>();
        var configurationService = new ConstantConfigurationService();
        httpClient
            .Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<VatRegistrationRequest>()))
            .Returns(Task.CompletedTask);
        var service = new TaxuallyService(httpClient.Object, queueClient.Object, configurationService);
        var action = () => service.RegisterUKVatNumber(new VatRegistrationRequest("MOCK", "MOCK", "GB")).Wait();

        // Act & Assert
        action.Should().NotThrow<ArgumentException>();
    }

    [TestMethod]
    public void ShouldNotAcceptNonUKCountryCode()
    {
        // Arragnge
        var httpClient = new Mock<IDomainHttpClient>();
        var queueClient = new Mock<IDomainQueueClient>();
        var configurationService = new ConstantConfigurationService();
        httpClient
            .Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<VatRegistrationRequest>()))
            .Returns(Task.CompletedTask);
        var service = new TaxuallyService(httpClient.Object, queueClient.Object, configurationService);
        var action = () => service.RegisterUKVatNumber(new VatRegistrationRequest("MOCK", "MOCK", "FR")).Wait();

        // Act & Assert
        action.Should().Throw<ArgumentException>("Invalid country code for UK VAT registration");
    }
}
