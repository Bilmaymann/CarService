using CarService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 1,
                    Company = "Chevrolet",
                    Model = "Cobalt",
                    Color = "Oq",
                    Price = 10000
                },
                new Car()
            {
                Id = 2,
                Company = "Chevrolet",
                Model = "Malibu",
                Color = "Qora",
                Price = 30000
            },
                new Car()
            {
                Id = 3,
                Company = "Chevrolet",
                Model = "Matiz",
                Color = "Qizil",
                Price = 5000
            },
                new Car()
            {
                Id = 4,
                Company = "Chevrolet",
                Model = "Neksiya",
                Color = "Ko`k",
                Price = 7000
            },
                new Car()
            {
                Id = 5,
                Company = "Chevrolet",
                Model = "Damas",
                Color = "Oq",
                Price = 10000
            }
            );
        }

        public DbSet<Car> Cars { get; set; }
    }
}
