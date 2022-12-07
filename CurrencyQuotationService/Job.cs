using CurrencyQuotationService.Api;
using CurrencyQuotationService.Models;
using CurrencyQuotationService.Providers;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace CurrencyQuotationService
{
    public class Job
    {
        private readonly Queue queue;
        private readonly CurrencyDataProvider currencyDataProvider;
        private readonly QuotationDataProvider quotationDataProvider;
        private readonly ILogger logger;

        public Job()
        {
            queue = new Queue();
            currencyDataProvider = new CurrencyDataProvider();
            quotationDataProvider = new QuotationDataProvider();
            logger = new Logger<Job>().Log;
        }

        public async Task Start()
        {
            logger.LogInformation($"Job started at {DateTime.Now}");
            try
            {
                var entry = await queue.GetEntry();
                if (entry == null) return;

                var currencyData = currencyDataProvider.GetCurrencyData(entry.Currency, entry.StartDate, entry.EndDate);
                var quotationData = quotationDataProvider.GetQuotationData(entry.Currency, entry.StartDate, entry.EndDate);

                var quotationReport = currencyData
                    .Where(it => quotationData.FirstOrDefault(q => 
                        GetDateForComparison(q.QuotationDate) == GetDateForComparison(it.ReferenceDate)) != null)
                    .Select(it => new QuotationReportModel
                    {
                        CurrencyId = entry.Currency,
                        ReferenceDate = it.ReferenceDate,
                        QuotationValue = quotationData.FirstOrDefault(q => 
                            GetDateForComparison(q.QuotationDate) == GetDateForComparison(it.ReferenceDate))?.QuotationValue ?? 0
                    }
                );

                var outFile = Path.Combine(
                    ConfigurationManager.AppSettings.Get("OutDir"),
                    $"Resultado_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.csv");

                using var stream = new StreamWriter(outFile, true);
                stream.WriteLine("ID_MOEDA;DATA_REF;VL_COTACAO");
                foreach (var rep in quotationReport)
                {
                    stream.WriteLine("{0};{1:yyyy-MM-dd};{2:0,00}", rep.CurrencyId, rep.ReferenceDate, rep.QuotationValue);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
            finally
            {
                logger.LogInformation($"Job finished at {DateTime.Now}");
            }
        }

        private static DateTime GetDateForComparison(DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);

    }
}
