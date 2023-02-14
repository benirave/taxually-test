
using Taxually.TechnicalTest.Builders;

namespace Taxually.TechnicalTest.Tests;
public class CsvBuilderTests
{
    [Fact]
    public void AddHeader_Adds_Header_ToBuilder()
    {
        var csvBuilder = new CsvBuilder();

        csvBuilder
            .AddHeader("Test Header 1")
            .AddHeader("Test Header 2");

        csvBuilder.Headers.Count.Should().Be(2);
        csvBuilder.Headers[0].Should().Be("Test Header 1");
        csvBuilder.Headers[1].Should().Be("Test Header 2");
    }

    [Fact]
    public void AddRow_Adds_Data_To_Column()
    {
        var csvBuilder = new CsvBuilder();

        csvBuilder
            .AddHeader("Test Header 1")
            .AddHeader("Test Header 2")
            .AddRow("Test Header 1", "data for header 1")
            .AddRow("Test Header 2", "data for header 2");

        csvBuilder.Headers.Count.Should().Be(2);
        csvBuilder.Headers[0].Should().Be("Test Header 1");
        csvBuilder.Headers[1].Should().Be("Test Header 2");

        csvBuilder.Values.Count.Should().Be(2);
        csvBuilder.Values["Test Header 1"].Should().Be("data for header 1");
        csvBuilder.Values["Test Header 2"].Should().Be("data for header 2");
    }

    [Fact]
    public void AddRow_Thorws_Exception_If_Column_DoesntExists()
    {
        var csvBuilder = new CsvBuilder();

        csvBuilder
            .AddHeader("Test Header 1")
            .AddHeader("Test Header 2")
            .AddRow("Test Header 1", "data for header 1");

        var act = () => csvBuilder.AddRow("Non existent header", "non");

        act.Should().ThrowExactly<Exception>().WithMessage("Header column not found");
    }

    [Fact]
    public void Build_Returns_Csv_String()
    {
        var csvBuilder = new CsvBuilder();

        var result = csvBuilder
            .AddHeader("Test Header 1")
            .AddHeader("Test Header 2")
            .AddRow("Test Header 1", "data for header 1")
            .AddRow("Test Header 2", "data for header 2")
            .Build();

        result.Should().Be($"Test Header 1,Test Header 2{Environment.NewLine}data for header 1,data for header 2{Environment.NewLine}");
    }

    [Fact]
    public void Build_Throws_Exception_If_Column_Or_Rows_Are_Not_Added()
    {
        var csvBuilder = new CsvBuilder();

        var act = () => csvBuilder.Build();

        act.Should().ThrowExactly<Exception>().WithMessage("Headers and Rows must be added.");
    }
}
