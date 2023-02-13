using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

/// <summary>
/// <see cref="IVatRegistrationService"/> implementation for the UK
/// </summary>
public class UnitedKingdomVatRegistrationService : IVatRegistrationService
{
    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        // UK has an API to register for a VAT number
        var httpClient = new TaxuallyHttpClient();
        await httpClient.PostAsync("https://api.uktax.gov.uk", request);
    }
}
