// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NicehashExporter;
using NicehashExporter.API;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((configuration) =>
        {
            configuration.Sources.Clear();

            configuration
                .AddEnvironmentVariables("NICEHASH_");

        })
        .ConfigureServices(c =>
        {
            c.AddSingleton<MetricCollection>();
            c.AddSingleton<NicehashClient>();
            c.AddSingleton<CoinDeskPoller>();
            c.AddHostedService<CoinDeskPoller>();
            c.AddHostedService<PrometheusExporter>();
            c.AddHostedService<NicehashPoller>();
        })
        .ConfigureLogging(c => c.AddConsole())
    ;



using IHost host = CreateHostBuilder(args)
    .Build();

await host.RunAsync();