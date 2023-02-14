using Taxually.TechnicalTest.Services.VatRegistrationServices;

namespace Taxually.TechnicalTest.Factories;

public interface IVatRegistrationFactory
{
    /// <summary>
    /// Gets VatRegistration service implementation by the contryCode
    /// </summary>
    IVatRegistrationService GetVatRegistrationService(string countryCode);
}
