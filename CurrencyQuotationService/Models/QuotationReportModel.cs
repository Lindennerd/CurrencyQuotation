using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuotationService.Models
{
    public class QuotationReportModel : CurrencyDataModel
    {
        public Decimal QuotationValue { get; set; }
    }
}
