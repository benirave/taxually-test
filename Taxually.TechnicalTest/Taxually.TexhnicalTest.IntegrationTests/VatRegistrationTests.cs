using FluentAssertions;
using Taxually.TexhnicalTest.IntegrationTests.Common;

namespace Taxually.TexhnicalTest.IntegrationTests;
public class VatRegistrationTests : TestBase
{
	private readonly DefaultWebApplicationFactory _factory;

	public VatRegistrationTests(DefaultWebApplicationFactory factory)
	{
		_factory = factory;
	}

	[Theory]
    [InlineData("HU", false)]
    [InlineData("GB", true)]
    [InlineData("FR", true)]
    [InlineData("DE", true)]
	public async Task Request_VatRegistration(string country, bool shouldSucceed)
	{
		// Arrange
		var client = _factory.CreateClient();

		var serviceClient = new VatRegistrationServiceClient(client);

		var request = new VatRegistrationRequest("Taxually", "123456", country);

		// Act
		var act = async () => await serviceClient.CreateVatRegistration(request, CancellationToken.None);

		// Assert
		if (shouldSucceed)
		{
            await act.Should().NotThrowAsync();
        }
        else
		{
			var exceptionDetails = (await act.Should().ThrowExactlyAsync<ApiClientException>().WithMessage("Country not supported"))
				.And.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
