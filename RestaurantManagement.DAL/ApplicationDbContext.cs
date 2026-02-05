using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }

        // Konstruktor dla DI
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Konstruktor bezparametrowy dla WPF
        public ApplicationDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RestaurantDB;Integrated Security=True;TrustServerCertificate=True;"
                );
            }
        }
    }
}
