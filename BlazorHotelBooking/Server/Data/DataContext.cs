using BlazorHotelBooking.Server.Models;
using BlazorHotelBooking.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlazorHotelBooking.Server.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreator != null)
            {
                if (!dbCreator.CanConnect())
                {
                    dbCreator.Create();
                }

                if (!dbCreator.HasTables())
                {
                    dbCreator.CreateTables();
                }

                
            }



        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelBooking> HotelBookings => Set<HotelBooking>();
        public DbSet<TourBooking> TourBookings => Set<TourBooking>();
        public DbSet<PackageBooking> PackageBookings => Set<PackageBooking>();
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Payments> Payments => Set<Payments>();

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

            modelBuilder.Entity<Tour>().HasData(
                new Tour
                {
                    Id = 1,
                    Name = "Real Britain",
                    Cost = 1200,
                    DurationInDays = 6,
                    MaxNumberOfGuests = 30,
                    Description = "Dive into charming villages, rolling hills, and iconic castles in this 6-day escape to authentic Britain"
                },
                new Tour
                {
                    Id = 2,
                    Name = "Britain and Ireland Explorer",
                    Cost = 2000,
                    DurationInDays = 16,
                    MaxNumberOfGuests = 40,
                    Description = "Journey through 16 days of cityscapes, dramatic coasts, and Celtic charm. Uncover the best of Britain and Ireland."
                },
                new Tour
                {
                    Id = 3,
                    Name = "Best of Britain",
                    Cost = 2900,
                    DurationInDays = 12,
                    MaxNumberOfGuests = 30,
                    Description = "Indulge in 12 days of luxury. Explore stately homes, savor Michelin stars, and discover hidden gems of Britain's finest"
                }
            );
        }
    }
}
