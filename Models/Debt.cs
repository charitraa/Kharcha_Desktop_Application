using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_tracker.Models
{
    public class Debt
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Notes { get; set; }
        public string Tags { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
    }
    
}
