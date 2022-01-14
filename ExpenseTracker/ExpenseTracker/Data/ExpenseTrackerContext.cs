using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain.Models;

namespace ExpenseTracker.Data
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext (DbContextOptions<ExpenseTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseTracker.Domain.Models.Category> Category { get; set; }

        public DbSet<ExpenseTracker.Domain.Models.Transaction> Transaction { get; set; }

        public DbSet<ExpenseTracker.Domain.Models.User> User { get; set; }
    }
}
