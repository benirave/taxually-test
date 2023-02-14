namespace Taxually.TechnicalTest.Models;

public class ErrorResult
{
    public Guid TraceId { get; } = Guid.NewGuid();

    public string Message => $"Please provide the following id {TraceId} to the support team.";

    public string ErrorMessage { get; init; }
}
