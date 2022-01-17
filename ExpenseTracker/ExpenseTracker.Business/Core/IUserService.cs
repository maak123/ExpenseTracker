using ExpenseTracker.Business.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Core
{
    public interface IUserService
    {
        Task<IEnumerable<UserResource>> GetAllAsync();
        Task<UserResource> GetByIdAsync(int id);
        Task<UserResource> CreateAsync(UserResource device);
        Task<UserResource> EditAsync(UserResource device);
        Task<Boolean> RemoveAsync(int id);
        Task<IEnumerable<UserResource>> SearchAsync(string hint);
    }
}
