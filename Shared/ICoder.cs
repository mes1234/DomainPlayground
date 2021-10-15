using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Shared
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
        /// <returns>fluent coder</returns>
        public ICoder Encode(object obj);

        /// <summary>
        /// Finilizes this instance.
        /// </summary>
        /// <returns></returns>
        public byte[] Finilize();
    }
}
