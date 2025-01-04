using System.Transactions;

namespace Expenses_tracker.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Password { get; set; }  // Will be stored hashed
        public String type { get; set; }
        public decimal TotalBalance { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
        public List<Debt> Debts { get; set; } = new();

    }

}
