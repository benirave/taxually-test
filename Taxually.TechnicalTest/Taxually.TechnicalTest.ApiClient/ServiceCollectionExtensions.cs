using Microsoft.Extensions.DependencyInjection;

namespace Taxually.TechnicalTest.ApiClient;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTaxuallyApiClient(this IServiceCollection services, Uri apiBaseAddress)
    {
        services.AddHttpClient<IVatRegistrationServiceClient, VatRegistrationServiceClient>(client =>
        {
            client.BaseAddress = apiBaseAddress;
        });

        return services;
    }
}
