using CurrencyQuotationService.MappingProfiles;
using CurrencyQuotationService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace CurrencyQuotationService.Providers
{
    public class QuotationDataProvider : CsvParserProvider<QuotationDataModel>
    {
             
        public QuotationDataProvider() : base(
            filePath: ConfigurationManager.AppSettings.Get("QuotationDataFile"),
            mapping: new QuotationDataMapping()
            )
        {}

        public IEnumerable<QuotationDataModel> GetQuotationData(string currency, DateTime start, DateTime end)
        {
            var code = CurrencyIdToCodeFactory(currency);
            return Data.Where(it => code == it.QuotationCode &&
                (it.QuotationDate >= start && it.QuotationDate <= end));
        }

        private int CurrencyIdToCodeFactory(string currencyId)
        {
            switch (currencyId)
            {
                case "AFN": return 66;
                case "ALL": return 49;
                case "ANG": return 33;
                case "ARS": return 3;
                case "AWG": return 6;
                case "BOB": return 56;
                case "BYN": return 64;
                case "CAD": return 25;
                case "CDF": return 58;
                case "CLP": return 16;
                case "COP": return 37;
                case "CRC": return 52;
                case "CUP": return 8;
                case "CVE": return 51;
                case "CZK": return 29;
                case "DJF": return 36;
                case "DZD": return 54;
                case "EGP": return 12;
                case "EUR": return 20;
                case "FJD": return 38;
                case "GBP": return 22;
                case "GEL": return 48;
                case "GIP": return 18;
                case "HTG": return 63;
                case "ILS": return 40;
                case "IRR": return 17;
                case "ISK": return 11;
                case "JPY": return 9;
                case "KES": return 21;
                case "KMF": return 19;
                case "LBP": return 42;
                case "LSL": return 4;
                case "MGA": return 35;
                case "MGB": return 26;
                case "MMK": return 69;
                case "MRO": return 53;
                case "MRU": return 15;
                case "MUR": return 7;
                case "MXN": return 41;
                case "MZN": return 43;
                case "NIO": return 23;
                case "NOK": return 62;
                case "OMR": return 34;
                case "PEN": return 45;
                case "PGK": return 2;
                case "PHP": return 24;
                case "RON": return 5;
                case "SAR": return 44;
                case "SBD": return 32;
                case "SGD": return 70;
                case "SLL": return 10;
                case "SOS": return 61;
                case "SSP": return 47;
                default:
                    return 0;
            }
        }
    }
}
