using System;
using System.Collections.Generic;
using System.Xml;

namespace Converter
{
    public class StringToXmlConverter
    {
        public XmlDocument Convert(IProvider provider, IStringCreator creator)
        {
            ValidateParameters(provider, creator);

            var document = new XmlDocument();
            var declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(declaration);

            var root = document.CreateElement("urlAddresses");

           AddChildElements(
                provider.GetData(), creator, root, document);

            document.AppendChild(root);

            return document;
        }

        private void ValidateParameters(IProvider provider, IStringCreator creator)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(
                    "The parameter can't be null",
                    nameof(provider));
            }

            if (creator == null)
            {
                throw new ArgumentNullException(
                    "The parameter can't be null",
                    nameof(creator));
            }
        }

        private void AddChildElements(
            IEnumerable<string> provider,
            IStringCreator creator,
            XmlNode node,
            XmlDocument document)
        {
            int i = 0;
            foreach (var data in provider)
            {
                i++;
                string xmlString;
               
                xmlString = creator.CreateString(data);

                var xmlDocumentFragment = document.CreateDocumentFragment();
                xmlDocumentFragment.InnerXml = xmlString;

                node.AppendChild(xmlDocumentFragment);
            }
        }
    }
}
