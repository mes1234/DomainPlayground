using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Doomain.Streaming
{


    /// <summary>
    /// Generic streaming utility
    /// </summary>
    public class RedisStreaming : BackgroundService, IStreaming
    {
        private readonly Guid _id = Guid.NewGuid();
        private static readonly ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect("localhost");
        private readonly IEnumerable<IStreamingHandler> _handlers;
        private readonly ILogger<RedisStreaming> _logger;
        private readonly IDatabase _db;


        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStreaming"/> class.
        /// </summary>
        /// <param name="handlers">handlers.</param>
        /// <param name="logger">logger</param>
        public RedisStreaming(
            IEnumerable<IStreamingHandler> handlers,
            ILogger<RedisStreaming> logger)
        {
            _handlers = handlers;
            _logger = logger;
            _db = _redis.GetDatabase();
        }

        /// <inheritdoc/>
        public async Task Publish(Topic topic, byte[] msg)
        {
            try
            {
                await _db.StreamAddAsync("generic", topic.GetTopic(), msg).ConfigureAwait(false);
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
            var msgCounter = "0-0";
            try
            {
                _db.StreamCreateConsumerGroup("generic", _id.ToString(), StreamPosition.Beginning);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var msg = await _db.StreamReadAsync("generic", msgCounter, 1).ConfigureAwait(false);
                    if (msg.Length != 0)
                    {
                        Handle(msg.First());
                        msgCounter = msg.First().Id;
                    }

                    await Task.Delay(1000).ConfigureAwait(false);
                }

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured {ex}", ex.Message);
                throw;
            }
        }

        private void Handle(StreamEntry streamEntry)
        {
            var topic = GetTopic(streamEntry.Values[0].Name);

            foreach (var handler in _handlers.Where(x => x.SupportedTopic == topic))
            {
                _logger.LogInformation("Handling data for topic {@topic}", topic);
                handler.Handle((byte[])streamEntry.Values[0].Value.Box());
            }
        }

        private Topic GetTopic(string natsSubject)
        {
            var subject = natsSubject.Split(".")[1];

            return subject switch
            {
                "addedorupdated" => Topic.AddOrUpdated,
                "deleted" => Topic.Delete,
                _ => throw new NotSupportedException($"subject {subject} is not supported"),
            };
        }
    }
}
