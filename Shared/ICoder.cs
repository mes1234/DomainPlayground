using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Interface used to encode/decode.
    /// </summary>
    public interface ICoder
    {
        /// <summary>
        /// Transform object to its byte representation.
        /// </summary>
        /// <param name="obj">object to transform</param>
        /// <returns>bytes.</returns>
        public byte[] GetBytes(object obj);
    }
}
