using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ExpenseTracker.Business.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
