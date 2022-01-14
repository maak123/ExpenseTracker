using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ExpenseTracker.Domain.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
    }
}
