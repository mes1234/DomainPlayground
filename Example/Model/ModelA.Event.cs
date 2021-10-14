using System;
using Doomain.Abstraction;
using Doomain.Shared;
using Doomain.Events;

namespace Example
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
        {
            _ = _coder.Encode(Id);
            return _coder.Encode(Name);
        }
    }
}
