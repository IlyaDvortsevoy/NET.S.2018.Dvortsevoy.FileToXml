using System.Collections.Generic;

namespace Converter
{
    public interface IProvider
    {
        IEnumerable<string> GetData();
    }
}
