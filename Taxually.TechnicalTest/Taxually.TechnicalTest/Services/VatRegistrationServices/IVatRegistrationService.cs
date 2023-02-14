using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services.VatRegistrationServices;

public interface IVatRegistrationService
{
    /// <summary>
    /// Registers the company in the given country
    /// </summary>
    Task RegisterCompany(VatRegistrationRequest request);
}
