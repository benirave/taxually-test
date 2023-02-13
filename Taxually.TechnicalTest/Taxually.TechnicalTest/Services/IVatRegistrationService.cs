using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services;

public interface IVatRegistrationService
{
    Task RegisterCompany(VatRegistrationRequest request);
}
