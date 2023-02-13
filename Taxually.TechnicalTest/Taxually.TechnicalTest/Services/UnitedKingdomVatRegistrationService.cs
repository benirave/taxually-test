using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

public class UnitedKingdomVatRegistrationService : IVatRegistrationService
{
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        // UK has an API to register for a VAT number
        var httpClient = new TaxuallyHttpClient();
        await httpClient.PostAsync("https://api.uktax.gov.uk", request);
    }
}
