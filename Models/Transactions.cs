﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_tracker.Models
{
    internal class Transactions

    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }


        public enum TransactionType
        {
            Income,
            Expense
        }
    }
    
}
