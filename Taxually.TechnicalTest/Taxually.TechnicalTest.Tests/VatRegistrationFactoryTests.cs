using Microsoft.Extensions.DependencyInjection;
using Taxually.TechnicalTest.Builders;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Factories;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Tests;
public class VatRegistrationFactoryTests
{
    private IServiceCollection _serviceCollection;

    public VatRegistrationFactoryTests()
    {
        _serviceCollection = new ServiceCollection()
            .AddTransient<IVatRegistrationService, UnitedKingdomVatRegistrationService>()
            .AddTransient<IVatRegistrationService, FranceVatRegistrationService>()
            .AddTransient<IVatRegistrationService, GermanyVatRegistrationService>()
            .AddTransient(_ => new Fake<ITaxuallyHttpClient>().FakedObject)
            .AddTransient(_ => new Fake<ITaxuallyQueueClient>().FakedObject)
            .AddTransient(_ => new Fake<ICsvBuilder>().FakedObject)
            .AddTransient(_ => new Fake<IXmlSerilazer>().FakedObject);
    }

    [Theory]
    [InlineData("GB", typeof(UnitedKingdomVatRegistrationService))]
    [InlineData("FR", typeof(FranceVatRegistrationService))]
    [InlineData("DE", typeof(GermanyVatRegistrationService))]
    public void GetVatRegistrationService_Returns_Correct_Implementation_Of_VatRegistrationService_By_CountryCode(string countryCode, Type serviceType)
    {
        using var serviceProvider = _serviceCollection.BuildServiceProvider();
        var factory = new VatRegistrationFactory(serviceProvider);

        var service = factory.GetVatRegistrationService(countryCode);

        service.Should().NotBeNull();
        service.Should().BeOfType(serviceType);
    }

    [Fact]
    public void GetVatRegistrationService_Throws_Exception_WHen_Implementation_Of_VatRegistrationService_By_CountryCode_Is_Missing()
    {
        var factory = new VatRegistrationFactory(_serviceCollection.BuildServiceProvider());

        var act = () => factory.GetVatRegistrationService("HU");

        act.Should().ThrowExactly<Exception>().WithMessage("Country not supported");
    }
}
