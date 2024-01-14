using Serilog.Events;
using Serilog;

namespace PokerPlanning.Configuration;

public static class SerilogConfig
{
    public static void AddSerilogConfig(this IHostBuilder builder)
    {
        builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                    .WriteTo.Console());

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }
}
