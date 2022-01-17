using ExpenseTracker.Business.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Core
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResource>> GetAllAsync(int userId);
        Task<TransactionResource> GetByIdAsync(int id);
        Task<TransactionResource> CreateAsync(TransactionAddUpdateResource device);
        Task<TransactionResource> EditAsync(TransactionAddUpdateResource device);
        Task<Boolean> RemoveAsync(int id);
        Task<IEnumerable<TransactionResource>> SearchAsync(string hint);
    }
}
