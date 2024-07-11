using Core.Models;
using Core.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseCore.DBContext
{
    public class EmployeeDBContext : DbContext
	{
        private readonly IOptions<DbConfig> _dbOptions;

        //public EmployeeDBContext(DbContextOptions options) : base(options)
        //{
        //}

        public EmployeeDBContext(DbContextOptions options, IOptions<DbConfig> dbOptions) : base(options)
        {
            _dbOptions = dbOptions;
        }

        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// It configures the database to be used for this context
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionstring = _dbOptions.Value.PathToDB; // @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\_Ashutosh\Trainings\SOLID\Ashutosh\SRP_DI\EmployeeManagementApi\oldEmployee.mdf;Integrated Security=True"; 
                optionsBuilder.UseSqlServer(connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(s => s.Id);

            modelBuilder.Entity<Employee>().HasDiscriminator<int>("EmpType")
                .HasValue<FullTimeEmployee>(1)
                .HasValue<PartTimeEmployee>(2);
        }
    }
}
