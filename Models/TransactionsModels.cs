namespace Expenses_tracker.Models
{
    public class Transactions
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Notes { get; set; }
        public string Tags { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }

       
    }
}
