using System.Xml.Serialization;

namespace Taxually.TechnicalTest.Services;

public class XmlSerilazer : IXmlSerilazer
{
    public string Serilaze<T>(T value)
    {
        using var stringwriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(T));

        serializer.Serialize(stringwriter, value);

        return stringwriter.ToString();
    }
}
