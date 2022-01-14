using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string title { get; set; }
        public string icon { get; set; }

    }
}
