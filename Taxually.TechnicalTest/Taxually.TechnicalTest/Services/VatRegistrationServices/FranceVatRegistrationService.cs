using System.Text;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services.VatRegistrationServices;

/// <summary>
/// <see cref="IVatRegistrationService"/> implementation for France
/// </summary>
public class FranceVatRegistrationService : IVatRegistrationService
{
    private readonly ITaxuallyQueueClient _queueClient;

    private readonly ICsvBuilder _csvBuilder;

    public FranceVatRegistrationService(ITaxuallyQueueClient queueClient, ICsvBuilder csvBuilder)
    {
        _queueClient = queueClient;
        _csvBuilder = csvBuilder;
    }

    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        var csv = _csvBuilder
            .AddHeader(nameof(VatRegistrationRequest.CompanyName))
            .AddHeader(nameof(VatRegistrationRequest.CompanyId))
            .AddRow(nameof(VatRegistrationRequest.CompanyName), request.CompanyName)
            .AddRow(nameof(VatRegistrationRequest.CompanyId), request.CompanyId)
            .Build();

        // Queue file to be processed
        await _queueClient.EnqueueAsync("vat-registration-csv", Encoding.UTF8.GetBytes(csv));
    }
}
