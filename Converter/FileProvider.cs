using System.Collections.Generic;
using System.IO;

namespace Converter
{
    public class FileProvider : IProvider
    {
        private string _path;

        public FileProvider(string path)
        {
            _path = path;
        }

        public IEnumerable<string> GetData()
        {
            var list = new List<string>();

            using (var reader = new StreamReader(
                File.Open(_path, FileMode.Open, FileAccess.Read)))
            {
                while (!reader.EndOfStream)
                {
                    list.Add(reader.ReadLine()?.Trim());
                }
            }

            return list;
        }
    }
}
