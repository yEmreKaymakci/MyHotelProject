namespace RapidApiConsume.Models
{
    public class ExchangeViewModel
    {
        public List<Exchange_Rates>? exchange_rates { get; set; }
        public string? currency { get; set; }
    }

    public class Exchange_Rates
    {
        public string? currency { get; set; }
        public string? code { get; set; }
        public string? full_name { get; set; }
        public string? buying { get; set; }
        public string? selling { get; set; }
    }
}
