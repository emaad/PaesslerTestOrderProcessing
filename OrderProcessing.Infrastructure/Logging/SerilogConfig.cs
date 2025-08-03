using Serilog;
using Serilog.Events;

namespace OrderProcessing.Infrastructure.Logging;

public static class SerilogConfig
{
    public static LoggerConfiguration Configure(LoggerConfiguration config) =>
        config
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
}