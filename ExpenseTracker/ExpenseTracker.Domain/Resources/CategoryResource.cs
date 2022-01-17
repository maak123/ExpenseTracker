using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Business.Resources
{
    public class CategoryResource
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
