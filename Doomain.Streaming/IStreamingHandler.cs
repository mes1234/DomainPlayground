using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="header">The header.</param>
        /// <param name="content">The content.</param>
        /// <returns>Status</returns>
        public Task<Status> Handle(byte[] header, byte[] content);
    }
}
