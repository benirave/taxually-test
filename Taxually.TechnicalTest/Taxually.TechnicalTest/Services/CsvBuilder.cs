using System.Text;

namespace Taxually.TechnicalTest.Builders;

public class CsvBuilder : ICsvBuilder
{
    private readonly StringBuilder _builder = new();

    public IList<string> Headers { get; private set; } = new List<string>();

    public IDictionary<string, string> Values { get; private set; } = new Dictionary<string, string>();

    /// <inheritdoc />
    public ICsvBuilder AddHeader(string name)
    {
        Headers.Add(name);

        return this;
    }

    /// <inheritdoc />
    public ICsvBuilder AddRow(string headerName, string value)
    {
        if (!Headers.Contains(headerName))
        {
            throw new Exception("Header column not found");
        }

        Values[headerName] = value;

        return this;
    }

    /// <inheritdoc />
    public string Build(char separator = ',')
    {
        if (Headers.Count == 0 || Values.Count == 0)
        {
            throw new Exception("Headers and Rows must be added.");
        }

        var header = string.Join(separator, Headers);

        _builder.AppendLine(header);

        var values = new List<string>();

        foreach (var headerColumn in Headers)
        {
            if (Values.ContainsKey(headerColumn))
            {
                values.Add(Values[headerColumn]);
            }
            else
            {
                values.Add(string.Empty);
            }
        }

        var row = string.Join(separator, values);

        _builder.AppendLine(row);

        return _builder.ToString();
    }
}