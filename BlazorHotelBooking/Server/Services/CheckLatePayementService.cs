using BlazorHotelBooking.Server.Data;
using Sgbj.Cron;

namespace BlazorHotelBooking.Server.Services
{


    // This service is used to check if a booking has been paid in full or not by a due date an cancel it if it has not been paid in full
    public class CheckLatePayementService : BackgroundService
    {
        private readonly DataContext _context;
        private const int generalDelay = 1 * 10 * 1000;

        public CheckLatePayementService(IServiceScopeFactory factory)
        {
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<DataContext>(); 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run every day at midnight
            using var timer = new CronTimer("0 0 * * *", TimeZoneInfo.Local);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                var hotelbookings = _context.HotelBookings.Where(x => x.PaidInfull == false & x.IsCancelled == false & x.PaymentDueDate < DateTime.Now).ToList();
                var tourbookings = _context.TourBookings.Where(x => x.PaidInfull == false & x.IsCancelled == false & x.PaymentDueDate < DateTime.Now).ToList();
                var packageBookings = _context.PackageBookings.Where(x => x.PaidInfull == false & x.IsCancelled == false & x.PaymentDueDate < DateTime.Now).ToList();

                foreach (var booking in hotelbookings)
                {
                    Console.WriteLine($"{booking.Id} has been cancelled");
                    booking.IsCancelled = true;
                    _context.HotelBookings.Update(booking);
                   
                }

                foreach (var booking in tourbookings)
                {
                    Console.WriteLine($"{booking.Id} has been cancelled");
                    booking.IsCancelled = true;
                    _context.TourBookings.Update(booking);
                }

                foreach (var booking in packageBookings)
                {
                    Console.WriteLine($"{booking.Id} has been cancelled");
                    booking.IsCancelled = true;
                    _context.PackageBookings.Update(booking);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
