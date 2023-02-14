namespace Taxually.TechnicalTest.Services;

public interface ICsvBuilder
{
    /// <summary>
    /// Add headers for a column
    /// </summary>
    ICsvBuilder AddHeader(string name);

    /// <summary>
    /// Adds a value to the column
    /// </summary>
    /// <exception cref="Exception">Thrown when header is not found</exception>
    ICsvBuilder AddRow(string headerName, string value);


    /// <summary>
    /// Builds the csv string
    /// </summary>
    /// <exception cref="Exception">Thrown if there are no headers or calues added to the builder</exception>
    string Build(char separator = ',');
}
