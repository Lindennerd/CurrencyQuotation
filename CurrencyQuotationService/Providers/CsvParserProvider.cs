using Microsoft.Extensions.Logging;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace CurrencyQuotationService.Providers
{
    public abstract class CsvParserProvider<T> where T : class, new()
    {
        public IEnumerable<T> Data { get; set; }
        private readonly CsvParserOptions csvParserOptions = new(true, ';');

        private readonly ILogger logger;


        public CsvParserProvider(string filePath, CsvMapping<T> mapping)
        {
            logger = new Logger<T>().Log;
            try
            {
                logger.LogInformation($"Started parsing the file for the model {typeof(T).Name}");
                var parser = new CsvParser<T>(csvParserOptions, mapping);
                var parseResults = parser.ReadFromFile(filePath, Encoding.ASCII).ToList();
                Data = parseResults.Select(r => r.Result);
                logger.LogInformation($"Successfully parsed the file for the model {typeof(T).Name}");

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
