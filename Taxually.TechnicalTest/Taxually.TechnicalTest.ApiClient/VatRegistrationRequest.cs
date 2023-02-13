namespace Taxually.TechnicalTest.ApiClient;

public class VatRegistrationRequest
{
    public string CompanyName { get; }

    public string CompanyId { get; }

    public string Country { get; }

    public VatRegistrationRequest(string companyName, string companyId, string country)
    {
        CompanyName = companyName;
        CompanyId = companyId;
        Country = country;
    }
}
