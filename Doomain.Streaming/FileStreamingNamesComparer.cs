using System.Collections.Generic;

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
