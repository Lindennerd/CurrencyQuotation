using CurrencyQuotationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace CurrencyQuotationService.MappingProfiles
{
    internal class CurrencyDataMapping : CsvMapping<CurrencyDataModel>
    {
        public CurrencyDataMapping() : base()
        {
            MapProperty(0, x => x.CurrencyId);
            MapProperty(1, x => x.ReferenceDate);
        }
    }
}
