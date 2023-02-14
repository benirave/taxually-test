using System.Net;

namespace Taxually.TechnicalTest.ApiClient;

public class ApiClientException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public ErrorResult? ErrorResult { get; set; }

    public ApiClientException(HttpStatusCode httpStatus, string message = "Something went wrong.") : base(message) 
    {
        StatusCode = httpStatus;
    }

    public ApiClientException(HttpStatusCode httpStatus, ErrorResult errorResult) : base(errorResult.ErrorMessage) 
    {
        StatusCode = httpStatus;
        ErrorResult = errorResult;
    }
}
