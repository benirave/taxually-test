namespace Taxually.TechnicalTest.Builders;

public interface ICsvBuilder
{
    ICsvBuilder AddHeader(string name);

    ICsvBuilder AddRow(string headerName, string value);

    string Build(char separator = ',');
}
