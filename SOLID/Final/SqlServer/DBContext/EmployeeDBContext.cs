using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.Extensions.Options;
using Core.Options;

namespace DataBaseCore.DBContext
{
    public class EmployeeDBContext : DbContext
	{
        private readonly IOptions<DbConfig> _dbOptions;

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
                var connectionstring = _dbOptions.Value.PathToDB; // @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\_Ashutosh\\Trainings\\SOLID\\Final\\employee.mdf;Integrated Security=True;Connect Timeout=30"; 
                optionsBuilder.UseSqlServer(connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasIndex(x => x.Id);

            modelBuilder.Entity<Employee>().HasDiscriminator<int>("EmpType")
                                            .HasValue<FullTimeEmployee>(1)
                                            .HasValue<PartTimeEmployee>(2);
        }
    }
}
