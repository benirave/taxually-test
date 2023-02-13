using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Factories;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IVatRegistrationFactory, VatRegistrationFactory>();

        services.AddTransient<IVatRegistrationService, UnitedKingdomVatRegistrationService>();
        services.AddTransient<IVatRegistrationService, FranceVatRegistrationService>();
        services.AddTransient<IVatRegistrationService, GermanyVatRegistrationService>();

        services.AddTransient<ITaxuallyHttpClient, TaxuallyHttpClient>();
        services.AddTransient<ITaxuallyQueueClient, TaxuallyQueueClient>();

        return services;
    }
}
