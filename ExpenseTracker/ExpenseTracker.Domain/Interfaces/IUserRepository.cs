using ExpenseTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
