using System.Net.Http.Json;
using System.Text.Json;

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
            var errorResult = JsonSerializer.Deserialize<ErrorResult>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (errorResult is null)
            {
                throw new ArgumentNullException(nameof(errorResult));
            }

            throw new ApiClientException(result.StatusCode, errorResult);
        }
    }
}
