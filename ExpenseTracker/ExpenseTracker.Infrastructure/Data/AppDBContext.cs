using ExpenseTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Transactions> Transaction { get; set; }
        public DbSet<User> User { get; set; }

    }
}
