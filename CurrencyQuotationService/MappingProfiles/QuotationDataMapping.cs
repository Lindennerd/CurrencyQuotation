using CurrencyQuotationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace CurrencyQuotationService.MappingProfiles
{
    internal class QuotationDataMapping : CsvMapping<QuotationDataModel>
    {
        public QuotationDataMapping()
        {
            MapProperty(0, x => x.QuotationValue);
            MapProperty(1, x => x.QuotationCode);
            MapProperty(2, x => x.QuotationDate);
        }

    }
}
