using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;
using Taxually.TechnicalTest.Services.VatRegistrationServices;

namespace Taxually.TechnicalTest.Tests;
public class GermanyVatRegistrationServiceTests
{
    private readonly Fake<ITaxuallyQueueClient> _queueClientFake = new Fake<ITaxuallyQueueClient>();

    private readonly Fake<IXmlSerilazer> _xmlSerilazer = new Fake<IXmlSerilazer>();

    [Fact]
    public async Task RegisterCompany_Enquess_Xml()
    {
        // Arrange
        var germanyVatRegistrationService = new GermanyVatRegistrationService(_queueClientFake.FakedObject, _xmlSerilazer.FakedObject);

        var request = new VatRegistrationRequest()
        {
            CompanyName = "Taxually",
            CompanyId = "1235678",
            Country = "GE"
        };

        // Act
        await germanyVatRegistrationService.RegisterCompany(request);

        // Assert
        _xmlSerilazer.CallsTo(x => x.Serilaze(request))
            .MustHaveHappenedOnceExactly();

        _queueClientFake.CallsTo(x => x.EnqueueAsync("vat-registration-xml", A<string>.That.IsNotNull()))
            .MustHaveHappenedOnceExactly();
    }
}
