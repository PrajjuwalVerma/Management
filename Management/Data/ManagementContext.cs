using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Management.Data
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().HasKey(x => x.ManagerId);
            modelBuilder.Entity<Employee>().HasKey(x => x.EmployeeId);
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

    }
}
