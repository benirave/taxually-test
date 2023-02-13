namespace Taxually.TechnicalTest.ApiClient;

public interface IVatRegistrationServiceClient
{
    Task CreateVatRegistration(VatRegistrationRequest request, CancellationToken cancellationToken);
}

