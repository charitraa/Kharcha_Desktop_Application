using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Expenses_tracker.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Password { get; set; }  // Will be stored hashed
        public CurrencyType type { get; set; }
        public decimal TotalBalance { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
        public List<Debt> Debts { get; set; } = new();

    }
    public enum CurrencyType
    {
        NepaliRupee,
        Euro,
        BritishPound,
        USDollar,
        IndianRupee,
        JapaneseYen,
    }
}
