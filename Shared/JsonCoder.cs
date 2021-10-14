using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Shared
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class JsonCoder : ICoder
    {
        /// <inheritdoc/>
        public byte[] Encode(object obj)
        {
            if (obj is Guid id)
            {
                return id.ToByteArray();
            }

            if (obj is string text)
            {
                return Encoding.UTF8.GetBytes(text);
            }

            throw new NotImplementedException();
        }
    }
}
