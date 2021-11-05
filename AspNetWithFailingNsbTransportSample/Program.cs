using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetWithFailingNsbTransportSample
{
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNServiceBus(ctx =>
                {
                    var config = new EndpointConfiguration("AutoRetrySample");
                    config.EnableInstallers();
                    var transport = config.UseTransport<RabbitMQTransport>();
                    transport.ConnectionString("host=localhost");
                    transport.UseConventionalRoutingTopology();

                    return config;
                })
                .ConfigureServices(collection => collection.AddHostedService<MyHostedService>());
    }

    class MyHostedService : IHostedService
    {
        IMessageSession messageSession;

        public MyHostedService(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
