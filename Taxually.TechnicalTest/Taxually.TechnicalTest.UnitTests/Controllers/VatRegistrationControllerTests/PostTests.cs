using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Net;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Domain;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.UnitTests.Controllers.VatRegistrationControllerTests;

[TestClass]
public class PostTests
{
    [TestMethod]
    public void ShouldAcceptUKCountryCode()
    {
        // Arrange
        var taxuallyService = new Mock<ITaxuallyService>();
        taxuallyService.Setup(x =>
            x.RegisterUKVatNumber(It.IsAny<VatRegistrationRequest>()))
            .Returns(Task.CompletedTask);
        var controller = new VatRegistrationController(taxuallyService.Object);

        // Act
        var result = controller.Post(new Dto.VatRegistrationRequestDto("Mock", "Mock", "GB")).Result;

        // Assert
        result.Should().BeAssignableTo<IStatusCodeActionResult>();
        var statusCodeResult = (IStatusCodeActionResult)result;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [TestMethod]
    public void ShouldNotAcceptUnknownCountryCode()
    {
        // Arrange
        var taxuallyService = new Mock<ITaxuallyService>();
        var controller = new VatRegistrationController(taxuallyService.Object);

        // Act
        var result = controller.Post(new Dto.VatRegistrationRequestDto("Mock", "Mock", "INVALID_COUNTRY")).Result;

        // Assert
        result.Should().BeAssignableTo<IStatusCodeActionResult>();
        var statusCodeResult = (IStatusCodeActionResult)result;
        statusCodeResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}
