using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuotationService.Models
{
    public class QuotationDataModel
    {
        public Decimal QuotationValue { get; set; }
        public int QuotationCode { get; set; }
        public DateTime QuotationDate { get; set; }
    }
}
