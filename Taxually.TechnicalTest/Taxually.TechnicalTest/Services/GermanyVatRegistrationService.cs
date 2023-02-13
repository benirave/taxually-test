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

    public GermanyVatRegistrationService(ITaxuallyQueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        using (var stringwriter = new StringWriter())
        {
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringwriter, request);
            var xml = stringwriter.ToString();

            // Queue xml doc to be processed
            await _queueClient.EnqueueAsync("vat-registration-xml", xml);
        }
    }
}
