using System.Net;

namespace Taxually.TechnicalTest.ApiClient;

internal class ApiClientException : Exception
{
	public ApiClientException(HttpStatusCode httpStatus, string message = "Something went wrong.") : base(message)
	{ }
}
