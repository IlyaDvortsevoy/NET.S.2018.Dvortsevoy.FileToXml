using System.Xml;

namespace Converter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new FileProvider(@"../../../Url.txt");

            var converter = new StringToXmlConverter();
            XmlDocument document = converter.Convert(
                provider,
                new XmlStringCreator());


        }
    }
}
