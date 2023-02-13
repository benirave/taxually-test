using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Tests;
public class FranceVatRegistrationServiceTests
{
    private readonly Fake<ITaxuallyQueueClient> _queueClientFake = new Fake<ITaxuallyQueueClient>();

    [Fact]
    public async Task RegisterCompany_Enquess_Csv()
    {
        // Arrange
        var franceVatRegistrationService = new FranceVatRegistrationService(_queueClientFake.FakedObject);

        var request = new VatRegistrationRequest()
        {
            CompanyName = "Taxually",
            CompanyId = "1235678",
            Country = "FR"
        };

        // Act
        await franceVatRegistrationService.RegisterCompany(request);

        // Assert
        _queueClientFake.CallsTo(x => x.EnqueueAsync("vat-registration-csv", A<byte[]>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
}
