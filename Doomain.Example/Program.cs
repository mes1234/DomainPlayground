using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Doomain.Abstraction;
using Doomain.EventsDispatcher;
using Doomain.Shared;
using Doomain.Streaming;
using MediatR.Extensions.Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;


namespace Doomain.Example
{
    /// <summary>
    /// Entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Gets the autofac container.
        /// </summary>
        /// <value>
        /// The autofac container.
        /// </value>
        public ILifetimeScope AutofacContainer { get; private set; }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .Enrich.FromLogContext()
              .WriteTo.Console(theme: AnsiConsoleTheme.Code)
              .CreateLogger();

            CreateHostBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
                {
                    // Register your own things directly with Autofac here. Don't
                    // call builder.Populate(), that happens in AutofacServiceProviderFactory
                    // for you.
                    builder.RegisterAssemblyModules(typeof(ModelAbstractionModule).Assembly);
                    builder.RegisterAssemblyModules(typeof(EventsDispatcherModule).Assembly);
                    builder.RegisterAssemblyModules(typeof(SharedModule).Assembly);
                    builder.RegisterAssemblyModules(typeof(StreamingModule).Assembly);
                    builder.RegisterMediatR(typeof(ModelAbstractionModule).Assembly);
                    builder.RegisterMediatR(typeof(EventsDispatcherModule).Assembly);
                    builder.RegisterType<ModelA>();
                })
                .UseSerilog()
                .Build()
                .Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddHostedService<FakeStreaming>();
                });
    }
}
