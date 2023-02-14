using System.Xml.Serialization;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

/// <summary>
/// <see cref="IVatRegistrationService"/> implementation for Germany
/// </summary>
public class GermanyVatRegistrationService : IVatRegistrationService
{
    private readonly ITaxuallyQueueClient _queueClient;

    private readonly IXmlSerilazer _xmlSerilazer;

    public GermanyVatRegistrationService(ITaxuallyQueueClient queueClient, IXmlSerilazer xmlSerilazer)
    {
        _queueClient = queueClient;
        _xmlSerilazer = xmlSerilazer;
    }

    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        var xml = _xmlSerilazer.Serilaze(request);

        // Queue xml doc to be processed
        await _queueClient.EnqueueAsync("vat-registration-xml", xml);
    }
}
