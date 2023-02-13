using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Factories;

public class VatRegistrationFactory : IVatRegistrationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public VatRegistrationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public IVatRegistrationService GetVatRegistrationService(string countryCode)
    {
        var varRegistrationServices = _serviceProvider.GetServices<IVatRegistrationService>();

        return countryCode switch
        {
            "GB" => varRegistrationServices.First(x => x.GetType() == typeof(UnitedKingdomVatRegistrationService)),
            "FR" => varRegistrationServices.First(x => x.GetType() == typeof(FranceVatRegistrationService)),
            "DE" => varRegistrationServices.First(x => x.GetType() == typeof(GermanyVatRegistrationService)),
            _ => throw new Exception("Country not supported")
        };
    }
}
