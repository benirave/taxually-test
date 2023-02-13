using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Tests;
public class UnitedKingdomVatRegistrationServiceTests
{
    private readonly Fake<ITaxuallyHttpClient> _httpClientFake = new Fake<ITaxuallyHttpClient>();

    [Fact]
    public async Task RegisterCompany_Posts_Request()
    {
        // Arrange
        var ukVatRegistrationService = new UnitedKingdomVatRegistrationService(_httpClientFake.FakedObject);

        var request = new VatRegistrationRequest()
        {
            CompanyName = "Taxually",
            CompanyId = "1235678",
            Country = "GE"
        };

        // Act
        await ukVatRegistrationService.RegisterCompany(request);

        // Assert
        _httpClientFake.CallsTo(x => x.PostAsync("https://api.uktax.gov.uk", request))
            .MustHaveHappenedOnceExactly();
    }
}
