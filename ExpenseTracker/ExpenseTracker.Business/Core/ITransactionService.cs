using ExpenseTracker.Business.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Core
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResource>> GetAllAsync();
        Task<TransactionResource> GetByIdAsync(int id);
        Task<TransactionResource> CreateAsync(TransactionResource device);
        Task<TransactionResource> EditAsync(TransactionResource device);
        Task<Boolean> RemoveAsync(int id);
        Task<IEnumerable<TransactionResource>> SearchAsync(string hint);
    }
}
