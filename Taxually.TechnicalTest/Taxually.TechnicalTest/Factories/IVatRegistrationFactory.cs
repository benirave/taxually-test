using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Factories;

public interface IVatRegistrationFactory
{
    IVatRegistrationService GetVatRegistrationService(string countryCode);
}
