using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTracker.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public virtual Category Category { get; set; }

    }
}
