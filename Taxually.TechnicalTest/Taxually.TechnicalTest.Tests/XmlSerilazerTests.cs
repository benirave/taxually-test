using System.Xml;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Tests;

public class XmlSerilazerTests
{
    [Fact]
    public void Serilaze_Returns_Valid_XmlString()
    {
        var serilazer = new XmlSerilazer();

        var result = serilazer.Serilaze(new Test("Hello world!"));

        result.Should().NotBeNull();
        
        var isValidXml = IsValidXml(result);
        isValidXml.Should().BeTrue();
    }

    private bool IsValidXml(string xmlString)
    {
        try
        {
            if (!string.IsNullOrEmpty(xmlString))
            {
                XmlDocument xmlDoc = new();
                xmlDoc.LoadXml(xmlString);

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (XmlException)
        {
            return false;
        }
    }
    public sealed class Test
    {
        public string Message { get; set; }

        public Test(string message)
        {
            Message = message;
        }
        public Test()
        {

        }
    }
}
