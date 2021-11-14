using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Doomain.Events;
using Doomain.Shared;
using Doomain.Streaming;
using MediatR;

namespace Doomain.EventsDispatcher
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class StoreEventNotificationHandler : INotificationHandler<StoreEventNotification>
    {
        private readonly IStreaming _streaming;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreEventNotificationHandler"/> class.
        /// </summary>
        /// <param name="streaming">The streaming.</param>
        public StoreEventNotificationHandler(IStreaming streaming)
        {
            _streaming = streaming;
        }

        /// <summary>Handles a notification</summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task Handle(StoreEventNotification notification, CancellationToken cancellationToken)
        {
            switch (notification.EventType)
            {
                case EventTypes.AddedOrUpdated:
                    await _streaming.Publish(Topic.AddOrUpdated, GetMessage(notification.Header, notification.Content)).ConfigureAwait(false);
                    break;
                case EventTypes.Deleted:
                    await _streaming.Publish(Topic.Delete, GetMessage(notification.Header, notification.Content)).ConfigureAwait(false);
                    break;
            }
        }

        private static byte[] GetMessage(byte[] header, byte[] content)
        {
            var headerSize = BitConverter.GetBytes(header.Length);
            var contentSize = BitConverter.GetBytes(content.Length);

            var msg = new byte[header.Length + content.Length + 8];

            using (var ms = new MemoryStream(msg))
            {
                ms.Write(headerSize);
                ms.Write(contentSize);
                ms.Write(header);
                ms.Write(content);
            }

            return msg;
        }
    }
}
