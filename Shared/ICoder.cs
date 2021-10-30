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
    public interface ICoder : IDisposable
    {
        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>fluent coder</returns>
        public ICoder Init(byte[] content);

        /// <summary>
        /// Transform object to its byte representation.
        /// </summary>
        /// <param name="obj">object to transform</param>
        /// <returns>fluent coder</returns>
        public ICoder Encode(object obj);

        /// <summary>
        /// Finilizes this instance.
        /// </summary>
        /// <returns>bytes</returns>
        public byte[] Finilize();

        /// <summary>
        /// Decodes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>fluent coder</returns>
        public ICoder Decode(out string obj);

        /// <summary>
        /// Decodes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>fluent coder</returns>
        public ICoder Decode(out Guid obj);

        /// <summary>
        /// Decodes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>fluent coder</returns>
        public ICoder Decode(out int obj);
    }
}
