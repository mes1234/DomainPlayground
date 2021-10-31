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
        public Type Type => typeof(ModelA);

        /// <inheritdoc/>
        public byte[] Serialize()
            => _coder
                .Encode(Id)
            .Encode(Revision)
                .Encode(Name)
                .Finilize();

        /// <inheritdoc/>
        public void Deserialize(byte[] content)
        {
            _coder
                .Init(content)
                .Decode(out _id)
                .Decode(out int revision)
                .Decode(out string name)
                .Finilize();

            Revision = revision;
            Name = name;
        }
    }
}
