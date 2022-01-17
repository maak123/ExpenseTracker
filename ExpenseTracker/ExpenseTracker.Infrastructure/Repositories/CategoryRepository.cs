using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Domain.Models;
using ExpenseTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDBContext context) : base(context)
        {
        }

        public AppDBContext AppDbContext => context as AppDBContext;

    }
}