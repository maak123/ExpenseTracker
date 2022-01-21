using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Business.Resources
{
    public class CategoryTransactionResource
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public double TransactionsTotal { get; set; }

    }
}
