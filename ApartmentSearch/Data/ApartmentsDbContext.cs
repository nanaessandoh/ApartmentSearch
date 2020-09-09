using ApartmentSearch.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApartmentSearch.Data
{
    public class ApartmentsDbContext : IdentityDbContext<ApartmentsUser>
    {
        public ApartmentsDbContext(DbContextOptions<ApartmentsDbContext> options) : base(options){ }

        public DbSet<ApartmentListing> ApartmentListings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Apartment" , Description = "Private residence in a building or community which is owned by a landlord." },
                new Category { Id = 2, Name = "Condo", Description = "A large building containing residential suites" },
                new Category { Id = 3, Name = "House", Description = "A building for human habitation, especially one that is lived in by a family or small group of people." },
                new Category { Id = 4, Name = "Townhouse", Description = "A tall, narrow, traditional row house, generally having three or more floors." }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}
