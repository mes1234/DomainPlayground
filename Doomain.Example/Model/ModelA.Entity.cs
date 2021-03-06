using System;

namespace Doomain.Example
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA
    {
        private Guid _id;

        /// <inheritdoc/>
        public Guid Id => _id;

        /// <inheritdoc/>
        public int Revision { get; set; }
    }
}
