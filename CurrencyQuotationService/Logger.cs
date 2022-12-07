using Microsoft.Extensions.Logging;

namespace CurrencyQuotationService
{
    public class Logger<T>
    {
        public ILogger Log { get; set; }

        public Logger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("ConsoleApp", LogLevel.Debug);
                    
            });

            Log = loggerFactory.CreateLogger<T>();
        }
    }
}
