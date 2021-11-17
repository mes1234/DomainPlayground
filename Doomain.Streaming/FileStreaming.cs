using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client;
using NATS.Client.JetStream;

namespace Doomain.Streaming
{
    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public class FileStreaming : BackgroundService, IStreaming
    {

        private readonly string path = Path.Combine(Path.GetTempPath(), "Doomain");

        private readonly IEnumerable<IStreamingHandler> _handlers;
        private readonly ILogger<FileStreaming> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStreaming"/> class.
        /// </summary>
        /// <param name="handlers">handlers.</param>
        /// <param name="logger">logger</param>
        public FileStreaming(
            IEnumerable<IStreamingHandler> handlers,
            ILogger<FileStreaming> logger)
        {
            _handlers = handlers;
            _logger = logger;
            Directory.CreateDirectory(path);
        }

        /// <inheritdoc/>
        public async Task Publish(Topic topic, byte[] msg)
        {
            try
            {
                var counter = Directory.GetFiles(path).Length + 1;

                await File.WriteAllBytesAsync(Path.Combine(path, $"{topic.GetTopic()}.{counter}"), msg);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error occured {ex}", ex.Message);
                throw;
            }
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var counter = 0;
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (counter == Directory.GetFiles(path).Length)
                    {
                        await Task.Delay(1000).ConfigureAwait(false);
                        continue;
                    }

                    var files = Directory
                        .GetFiles(path)
                        .ToList();

                    files.Sort(new FileStreamingNamesComparer());

                    var newFile = files[counter];

                    counter++;

                    var data = await File.ReadAllBytesAsync(newFile, stoppingToken).ConfigureAwait(false);

                    var topic = GetTopic(Path.GetFileName(newFile));

                    foreach (var handler in _handlers.Where(x => x.SupportedTopic == topic))
                    {
                        _logger.LogInformation("Handling data for topic {@topic}", topic);
                        await handler.Handle(data).ConfigureAwait(false);
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured {ex}", ex.Message);
                throw;
            }
        }

        private Topic GetTopic(string fileName)
        {
            var subject = fileName.Split(".")[1];

            return subject switch
            {
                "addedorupdated" => Topic.AddOrUpdated,
                "deleted" => Topic.Delete,
                _ => throw new NotSupportedException($"subject {subject} is not supported"),
            };
        }
    }
}
