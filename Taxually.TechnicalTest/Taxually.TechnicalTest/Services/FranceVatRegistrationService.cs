using System.Text;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

/// <summary>
/// <see cref="IVatRegistrationService"/> implementation for France
/// </summary>
public class FranceVatRegistrationService : IVatRegistrationService
{
    private readonly ITaxuallyQueueClient _queueClient;

    public FranceVatRegistrationService(ITaxuallyQueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        // France requires an excel spreadsheet to be uploaded to register for a VAT number
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        // Queue file to be processed
        await _queueClient.EnqueueAsync("vat-registration-csv", csv);
    }
}
