using System;
using Doomain.Shared;

namespace Doomain.Example
{
    /// <summary>
    /// Some model of A
    /// </summary>
    public partial class ModelA : IEntity, IEvent
    {
        private readonly ICoder _coder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelA"/> class.
        /// </summary>
        /// <param name="coder">The coder.</param>
        public ModelA(ICoder coder)
        {
            _id = Guid.NewGuid();
            _coder = coder;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            Name = name;
        }
    }
}
