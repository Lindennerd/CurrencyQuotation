using System.Text.Json.Serialization;

namespace CurrencyQuotationService.Api.Model
{
    public class EntryModel
    {
        [JsonPropertyName("moeda")]
        public string Currency { get; set; }
        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("data_fim")]
        public DateTime EndDate { get; set; }
    }
}

