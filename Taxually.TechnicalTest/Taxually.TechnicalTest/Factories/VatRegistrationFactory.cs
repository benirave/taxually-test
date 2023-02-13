using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Factories;

public class VatRegistrationFactory : IVatRegistrationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public VatRegistrationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IVatRegistrationService GetVatRegistrationService(string countryCode)
    {
        var varRegistrationServices = _serviceProvider.GetServices<IVatRegistrationService>();

        return countryCode switch
        {
            "GB" => varRegistrationServices.First(x => x.GetType() == typeof(UnitedKingdomVatRegistrationService)),
            "FR" => varRegistrationServices.First(x => x.GetType() == typeof(FrenchVatRegistrationService)),
            "DE" => varRegistrationServices.First(x => x.GetType() == typeof(GermanVatRegistrationService)),
            _ => throw new Exception("Country not supported")
        };
    }
}
