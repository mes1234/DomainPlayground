using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Doomain.Streaming
{
    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public class MemoryStreaming : BackgroundService, IStreaming
    {
        private readonly IEnumerable<IStreamingHandler> _handlers;
        private readonly ILogger<MemoryStreaming> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryStreaming"/> class.
        /// </summary>
        /// <param name="handlers">handlers.</param>
        /// <param name="logger">logger</param>
        public MemoryStreaming(
            IEnumerable<IStreamingHandler> handlers,
            ILogger<MemoryStreaming> logger)
        {
            _handlers = handlers;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task Publish(Topic topic, byte[] header, byte[] content)
        {
            foreach (var handler in _handlers.Where(x => x.SupportedTopic == topic))
            {
                _logger.LogInformation("Handling publish for topic {@topic}", topic);
                await handler.Handle(header, content);
            }
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
