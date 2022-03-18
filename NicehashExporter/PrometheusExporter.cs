using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Prometheus;

namespace NicehashExporter
{
    public class PrometheusExporter : IHostedService
    {
        private readonly KestrelMetricServer _server;

        public PrometheusExporter(IConfiguration configuration)
        {
            Metrics.SuppressDefaultMetrics();
            _server = new KestrelMetricServer(hostname: configuration.GetValue("exporterHost", "+"),
                                              port: configuration.GetValue("exporterPort", 8088)
            );
        }

        public async Task StartAsync(CancellationToken cancellationToken) => _server.Start();

        public async Task StopAsync(CancellationToken cancellationToken) => await _server.StopAsync();
    }
}
