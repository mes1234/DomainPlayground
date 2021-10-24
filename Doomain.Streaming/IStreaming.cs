using System;
using System.Threading.Tasks;

namespace Doomain.Streaming
{
    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public interface IStreaming
    {
        /// <summary>
        /// Publishes the specified topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="content">The content.</param>
        public Task Publish(Topic topic, byte[] content);

    }
}
