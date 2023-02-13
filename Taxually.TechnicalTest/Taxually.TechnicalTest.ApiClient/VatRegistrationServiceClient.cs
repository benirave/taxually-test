using System.Net.Http.Json;

namespace Taxually.TechnicalTest.ApiClient;

internal sealed class VatRegistrationServiceClient : IVatRegistrationServiceClient
{
    private readonly HttpClient _httpClient;

    public VatRegistrationServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateVatRegistration(VatRegistrationRequest request, CancellationToken cancellationToken)
    {
        var result = await _httpClient.PostAsJsonAsync("api/VatRegistration", request, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            throw new ApiClientException(result.StatusCode);
        }
    }
}
