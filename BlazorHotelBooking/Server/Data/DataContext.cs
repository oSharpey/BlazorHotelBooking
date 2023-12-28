using BlazorHotelBooking.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BlazorHotelBooking.Server.Models;

namespace BlazorHotelBooking.Server.Data
{
    public class DataContext: IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelBooking> HotelBookings => Set<HotelBooking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {




            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "1",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "2",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });



            modelBuilder.Entity<Hotel>().HasData(
                 new Hotel
                 {
                     Id = 1,
                     Name = "Hilton London Hotel",
                     SBPrice = 375,
                     DBPrice = 775,
                     FamPrice = 950,
                     Description = "Experience luxury in the heart of the city, with sophisticated rooms and a stone's throw from London's major attractions and shopping districts."
                 },

                new Hotel
                {
                    Id = 2,
                    Name = "London Marriott Hotel",
                    SBPrice = 300,
                    DBPrice = 500,
                    FamPrice = 900,
                    Description = "Indulge in elegance and comfort at this centrally located hotel, featuring top-notch amenities and easy access to London's historical landmarks"
                },

                new Hotel
                {
                    Id = 3,
                    Name = "Travelodge Brighton Seafront",
                    SBPrice = 80,
                    DBPrice = 120,
                    FamPrice = 150,
                    Description = " Enjoy affordable comfort with stunning seafront views, ideally situated for exploring Brighton’s vibrant beach and pier attractions."
                },

                new Hotel
                {
                    Id = 4,
                    Name = "Kings Hotel Brighton",
                    SBPrice = 180,
                    DBPrice = 400,
                    FamPrice = 520,
                    Description = "A charming, budget-friendly hotel on Brighton’s seafront, offering cozy accommodations with easy access to the city's lively nightlife and cultural sites"
                },

                new Hotel
                {
                    Id = 5,
                    Name = "Leonardo Hotel Brighton",
                    SBPrice = 180,
                    DBPrice = 400,
                    FamPrice = 520,
                    Description = "Modern and stylish, this hotel provides a comfortable base to discover Brighton, conveniently close to the beach and the buzzing city center."
                },

                new Hotel
                {
                    Id = 6,
                    Name = "Nevis Bank Inn, Fort William",
                    SBPrice = 90,
                    DBPrice = 100,
                    FamPrice = 155,
                    Description = "Nestled in the Scottish Highlands, this inn offers a serene getaway with scenic views, perfect for outdoor enthusiasts and nature lovers"
                }
            );
        }
    }
}
