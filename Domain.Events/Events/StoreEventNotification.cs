using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Doomain.Shared;
using MediatR;

namespace Doomain.Events
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class StoreEventNotification : INotification
    {
        private readonly ICoder _coder;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreEventNotification"/> class.
        /// </summary>
        /// <param name="coder">coder</param>
        public StoreEventNotification(ICoder coder)
        {
            _coder = coder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreEventNotification"/> class.
        /// </summary>
        /// <param name="coder">The coder.</param>
        /// <param name="header">The header.</param>
        /// <param name="content">The content.</param>
        public StoreEventNotification(ICoder coder, byte[] header, byte[] content)
        {
            _coder = coder;

            Content = content;
            _coder
                .Init(header)
                .Decode(out Guid id)
                .Decode(out int revision)
                .Decode(out string contentType)
                .Decode(out int eventType);

            Id = id;

            Revision = revision;

            EventType = (EventTypes)eventType;

            ContentType = GetContentType(contentType);
        }

        /// <summary>
        /// Gets the type of the event.</summary>
        /// <value>The type of the event.</value>
        public EventTypes EventType { get; init; }

        /// <summary>
        /// Gets the type of the content.</summary>
        /// <value>The type of the content.</value>
        public Type ContentType { get; init; }

        /// <summary>
        /// Gets the content.</summary>
        /// <value>The content.</value>
        public byte[] Content { get; init; }

        /// <summary>
        /// Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets  the revision.
        /// </summary>
        /// <value>
        /// The revision.
        /// </value>
        public int Revision { get; init; }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public byte[] Header => SerializeHeader();

        private byte[] SerializeHeader() =>
             _coder
                  .Encode(Id)
                  .Encode(Revision)
                  .Encode(ContentType.FullName)
                  .Encode((int)EventType)
                  .Finilize();

        private Type GetContentType(string contentTypeName)
        {
            Type contentType;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith("System."))
                    continue;
                contentType = assembly.GetType(contentTypeName);
                if (contentType != null)
                    return contentType;
            }

            throw new KeyNotFoundException($"Cannot find Type {contentTypeName}");
        }
    }
}
