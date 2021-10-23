using System;
using Doomain.Abstraction;
using Doomain.Events;
using Doomain.Shared;

namespace Doomain.Example
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA
    {
        /// <inheritdoc/>
        public ContentTypes Type => ContentTypes.ModelA;

        /// <inheritdoc/>
        public byte[] Serialize()
            => _coder
                .Encode(Id)
                .Encode(Name)
                .Finilize();

        /// <inheritdoc/>
        public void Deserialize(byte[] content)
        {
        }
    }
}
