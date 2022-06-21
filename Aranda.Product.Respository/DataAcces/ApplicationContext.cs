using Microsoft.EntityFrameworkCore;

namespace Aranda.Product.Respository.DataAcces
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Infraestructure.Models.Product> Product { get; set; }
        public DbSet<Infraestructure.Models.Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Infraestructure.Models.Product>().ToTable("Product");

            modelBuilder.Entity<Infraestructure.Models.Category>()
                .HasMany(c=> c.Products)
                .WithOne(e=> e.Category)
                .IsRequired();
        }
    }
}
