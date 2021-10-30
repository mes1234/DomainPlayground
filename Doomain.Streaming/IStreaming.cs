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
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.</returns>
        public Task Publish(Topic topic, byte[] content);
    }
}
