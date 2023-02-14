using Taxually.TechnicalTest.Builders;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Tests;
public class FranceVatRegistrationServiceTests
{
    private readonly Fake<ITaxuallyQueueClient> _queueClientFake = new Fake<ITaxuallyQueueClient>();

    private readonly Fake<ICsvBuilder> _csvBuilderFake = new Fake<ICsvBuilder>();

    [Fact]
    public async Task RegisterCompany_Enquess_Csv()
    {
        _csvBuilderFake.CallsTo(x => x.AddHeader(A<string>._))
            .Returns(_csvBuilderFake.FakedObject);

        _csvBuilderFake.CallsTo(x => x.AddRow(A<string>._, A<string>._))
            .Returns(_csvBuilderFake.FakedObject);

        // Arrange
        var franceVatRegistrationService = new FranceVatRegistrationService(_queueClientFake.FakedObject, _csvBuilderFake.FakedObject);

        var request = new VatRegistrationRequest()
        {
            CompanyName = "Taxually",
            CompanyId = "1235678",
            Country = "FR"
        };

        // Act
        await franceVatRegistrationService.RegisterCompany(request);

        // Assert
        _csvBuilderFake.CallsTo(x => x.AddHeader(nameof(VatRegistrationRequest.CompanyName)))
            .MustHaveHappenedOnceExactly();
        _csvBuilderFake.CallsTo(x => x.AddHeader(nameof(VatRegistrationRequest.CompanyId)))
            .MustHaveHappenedOnceExactly();

        _csvBuilderFake.CallsTo(x => x.AddRow(nameof(VatRegistrationRequest.CompanyName), request.CompanyName))
            .MustHaveHappenedOnceExactly();
        _csvBuilderFake.CallsTo(x => x.AddRow(nameof(VatRegistrationRequest.CompanyId), request.CompanyId))
            .MustHaveHappenedOnceExactly();

        _csvBuilderFake.CallsTo(x => x.Build(A<char>._))
            .MustHaveHappenedOnceExactly();

        _queueClientFake.CallsTo(x => x.EnqueueAsync("vat-registration-csv", A<byte[]>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
}
