namespace Taxually.TechnicalTest.Services;

public interface IXmlSerilazer
{
    /// <summary>
    /// Serilazes T to an xml string
    /// </summary>
    string Serilaze<T>(T value);
}
