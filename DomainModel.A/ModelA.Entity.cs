using System;
using DomainModel.Abstraction;
using Events;

namespace DomainModel.A
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA
    {
        private readonly Guid _id;

        /// <inheritdoc/>
        public Guid Id => _id;


    }
}
