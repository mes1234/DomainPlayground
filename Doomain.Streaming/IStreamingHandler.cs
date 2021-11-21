using System.Threading.Tasks;

namespace Doomain.Streaming
{
    /// <summary>
    /// Handler which handles specific topic messages
    /// </summary>
    public interface IStreamingHandler
    {
        /// <summary>
        /// Gets the supported topic.
        /// </summary>
        /// <value>
        /// The supported topic.
        /// </value>
        public Topic SupportedTopic { get; }

        /// <summary>
        /// Handles the specified content.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Status</returns>
        public Task<Status> Handle(byte[] data);
    }
}
