using ExpenseTracker.Business.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Core
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResource>> GetAllAsync();
        Task<CategoryResource> GetByIdAsync(int id);
        Task<CategoryResource> CreateAsync(CategoryResource device);
        Task<CategoryResource> EditAsync(CategoryResource device);
        Task<Boolean> RemoveAsync(int id);
        Task<IEnumerable<CategoryResource>> SearchAsync(string hint);
    }
}
