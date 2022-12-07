using CurrencyQuotationService.MappingProfiles;
using CurrencyQuotationService.Models;
using System.Configuration;

namespace CurrencyQuotationService.Providers
{
    public class CurrencyDataProvider : CsvParserProvider<CurrencyDataModel>
    {
        public CurrencyDataProvider() : base(
            filePath: ConfigurationManager.AppSettings.Get("CurrencyDataFile"), 
            mapping: new CurrencyDataMapping())
        {}

        public IEnumerable<CurrencyDataModel> GetCurrencyData(string currency, DateTime start, DateTime end)
        {
            return Data
                .Where(
                    x => x.CurrencyId == currency 
                    && (x.ReferenceDate >= start 
                        && x.ReferenceDate <= end));
        }
    }
}
