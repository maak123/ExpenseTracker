using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Business.Resources
{
    public class TransactionAddUpdateResource
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
    }
}
