using System;
using DomainModel.Abstraction;
using Events;
using Shared;

namespace DomainModel.A
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA
    {

        /// <inheritdoc/>
        public ContentTypes Type => ContentTypes.ModelA;


        /// <inheritdoc/>
        public byte[] Serialize(ICoder coder)
        {
            _ = coder.Encode(Id);
            return coder.Encode(Name);
        }
    }
}
