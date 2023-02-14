using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services.VatRegistrationServices;

/// <summary>
/// <see cref="IVatRegistrationService"/> implementation for the UK
/// </summary>
public class UnitedKingdomVatRegistrationService : IVatRegistrationService
{
    private readonly ITaxuallyHttpClient _httpClient;

    public UnitedKingdomVatRegistrationService(ITaxuallyHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task RegisterCompany(VatRegistrationRequest request)
    {
        // UK has an API to register for a VAT number
        await _httpClient.PostAsync("https://api.uktax.gov.uk", request);
    }
}
