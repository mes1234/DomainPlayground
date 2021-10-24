using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomain.Streaming
{
    /// <summary>
    /// Topics
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// The add or updated stream
        /// </summary>
        public static Topic AddOrUpdated = new("generic", "addedorupdated");
        private readonly string _stream;
        private readonly string _topic;

        /// <summary>
        /// Initializes a new instance of the <see cref="Topic"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="topic">The topic.</param>
        public Topic(string stream, string topic)
        {
            _stream = stream;
            _topic = topic;
        }
    }
}
