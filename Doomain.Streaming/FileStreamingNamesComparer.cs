using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Streaming
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class FileStreamingNamesComparer : IComparer<string>
    {
            /// <inheritdoc/>
            public int Compare(string x, string y) => int.Parse(x.Split(".")[2]) - int.Parse(y.Split(".")[2]);
    }
}
