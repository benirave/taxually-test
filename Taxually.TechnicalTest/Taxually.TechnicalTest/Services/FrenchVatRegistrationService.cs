using System.Text;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

public class FrenchVatRegistrationService : IVatRegistrationService
{
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        // France requires an excel spreadsheet to be uploaded to register for a VAT number
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
        var excelQueueClient = new TaxuallyQueueClient();

        // Queue file to be processed
        await excelQueueClient.EnqueueAsync("vat-registration-csv", csv);
    }
}
