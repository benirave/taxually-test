using FakeItEasy;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Tests;
public class GermanyVatRegistrationServiceTests
{
    private readonly Fake<ITaxuallyQueueClient> _queueClientFake = new Fake<ITaxuallyQueueClient>();

    [Fact]
    public async Task RegisterCompany_Enquess_Xml()
    {
        // Arrange
        var germanyVatRegistrationService = new GermanyVatRegistrationService(_queueClientFake.FakedObject);

        var request = new VatRegistrationRequest()
        {
            CompanyName = "Taxually",
            CompanyId = "1235678",
            Country = "GE"
        };

        // Act
        await germanyVatRegistrationService.RegisterCompany(request);

        // Assert
        _queueClientFake.CallsTo(x => x.EnqueueAsync("vat-registration-xml", A<string>.That.IsNotNull()))
            .MustHaveHappenedOnceExactly();
    }
}
