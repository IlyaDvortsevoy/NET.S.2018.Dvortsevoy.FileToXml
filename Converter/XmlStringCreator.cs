using System;
using System.Text;
using System.Web;

namespace Converter
{
    public class XmlStringCreator : IStringCreator
    {
        public string CreateString(string input)
        {
            ValidateParameter(input);

            var result = new StringBuilder();
            var uri = new Uri(input);

            result.Append("<urlAddress>");
            result.Append($"<host name=\"{uri.Host}\"/>");
            ParseSegments(result, uri);
            ParseParameters(result, uri);
            result.Append("</urlAddress>");

            return result.ToString();
        }

        private void ValidateParameter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(
                    $"Invalid {nameof(input)} parameter");
            }
        }

        private static void ParseSegments(StringBuilder result, Uri uri)
        {
            if (uri.Segments.Length > 1)
            {
                result.Append("<uri>");
                foreach (var segment in uri.Segments)
                {
                    var temp = segment.Trim('/', ' ');
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        result.Append($"<segment>{temp}</segment>");
                    }
                }

                result.Append("</uri>");
            }
        }

        private static void ParseParameters(StringBuilder result, Uri uri)
        {
            if (string.IsNullOrWhiteSpace(uri.Query))
            {
                return;
            }

            result.Append("<parameters>");

            var collection = HttpUtility.ParseQueryString(uri.Query);

            foreach (var key in collection.AllKeys)
            {
                var xmlParameter = $"<parameter value=\"{collection[key]}\" key=\"{key}\"/>";
                result.Append(xmlParameter);
            }

            result.Append("</parameters>");
        }
    }
}
