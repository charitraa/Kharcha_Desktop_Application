namespace Expenses_tracker.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Password { get; set; }  // Will be stored hashed
        public string type { get; set; }
        public decimal TotalBalance { get; set; }
        public List<Transactions> Transactions { get; set; } = new List<Transactions>();
        public List<DebtModels> Debts { get; set; } = new List<DebtModels> ();

    }

}
