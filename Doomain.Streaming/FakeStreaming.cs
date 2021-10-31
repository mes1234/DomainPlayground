using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Doomain.Streaming
{
    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public class FakeStreaming : BackgroundService, IStreaming
    {
        private readonly IEnumerable<IStreamingHandler> handlers;
        private readonly ILogger<FakeStreaming> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeStreaming"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public FakeStreaming(
            IServiceProvider serviceProvider,
            ILogger<FakeStreaming> logger)
        {
            handlers = serviceProvider.GetServices<IStreamingHandler>();
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task Publish(Topic topic, byte[] header, byte[] content)
        {
            foreach (var handler in handlers.Where(x => x.SupportedTopic == topic))
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
