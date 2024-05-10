using BlazorHotelBooking.Server.Data;
using BlazorHotelBooking.Shared;
using BlazorHotelBooking.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlazorHotelBooking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class BookingsController : ControllerBase
    {

        private readonly DataContext _context;


        public BookingsController(DataContext context)
        {
            _context = context;
        }



        //Bookking for Hotels
        [HttpGet("hotel/userbooking")]
        public async Task<ActionResult<List<HotelBookingViewModel>>> GetAllHotelBookingsWithUserId(string userId)
        {
            var query = from hotel in _context.Hotels
                        join booking in _context.HotelBookings on hotel.Id equals booking.HotelId
                        where booking.UserId == userId
                        select new HotelBookingViewModel
                        {
                            bookingId = booking.Id,
                            hotelName = hotel.Name,
                            RoomType = booking.RoomType,
                            CheckIn = booking.CheckIn,
                            CheckOut = booking.CheckOut,
                            NumberOfNights = booking.NumberOfNights,
                            TotalPrice = booking.TotalPrice,
                            DepositAmountPaid = booking.DepositAmountPaid,
                            BookingDate = booking.BookingDate,
                            paidInfull = booking.PaidInfull,
                            IsCancelled = booking.IsCancelled,
                            PaymentDueDate = booking.PaymentDueDate
                        };
            var result = await query.ToListAsync();

            return Ok(result);
        }

        [HttpPut("hotel/payment/{id}")]
        public async Task<ActionResult<string>> PayHotelRemainder(string id)
        {
            var dbHotel = await _context.HotelBookings.FindAsync(id);
            Payments payment = new Payments();

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            if (dbHotel.PaidInfull == true)
            {
                return BadRequest("You have already paid in full");
            }

            //dbHotel.DepositAmountPaid = dbHotel.TotalPrice;
            dbHotel.PaidInfull = true;

            payment.UserId = dbHotel.UserId;
            payment.bookingId = dbHotel.Id;
            payment.bookingType = "Hotel";
            payment.AmountPaid = dbHotel.TotalPrice - dbHotel.DepositAmountPaid;
            payment.paymentType = "PayRemainder";

            _context.Payments.Add(payment);


            _context.HotelBookings.Update(dbHotel);
            await _context.SaveChangesAsync();

            return Ok("Payment Successful");
        }

        [HttpPut("hotel/{id}")]
        public async Task<ActionResult<string>> UpdateHotel(string id, HotelBooking hotl, decimal paymentRemainder, decimal surcharge)
        {
            var dbHotel = await _context.HotelBookings.FindAsync(id);
            Payments payment = new Payments();
            Payments payment2 = new Payments();

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            dbHotel.Id = hotl.Id;
            dbHotel.HotelId = hotl.HotelId;
            dbHotel.RoomType = hotl.RoomType;
            dbHotel.CheckIn = hotl.CheckIn;
            dbHotel.CheckOut = hotl.CheckOut;
            dbHotel.NumberOfNights = hotl.NumberOfNights;
            dbHotel.TotalPrice = hotl.TotalPrice;
            dbHotel.DepositAmountPaid = hotl.DepositAmountPaid;
            dbHotel.BookingDate = hotl.BookingDate;
            dbHotel.UserId = hotl.UserId;
            dbHotel.PaidInfull = hotl.PaidInfull;
            dbHotel.IsCancelled = hotl.IsCancelled;
            dbHotel.PaymentDueDate = hotl.PaymentDueDate;


            payment.UserId = dbHotel.UserId;
            payment.bookingId = dbHotel.Id;
            payment.bookingType = "Hotel";
            payment.AmountPaid = paymentRemainder;
            payment.paymentType = "ModifyBooking";

            payment2.UserId = dbHotel.UserId;
            payment2.bookingId = dbHotel.Id;
            payment2.bookingType = "Hotel";
            payment2.AmountPaid = surcharge;
            payment2.paymentType = "Surcharge";

            _context.Payments.Add(payment2);
            _context.Payments.Add(payment);

            _context.HotelBookings.Update(dbHotel);
            await _context.SaveChangesAsync();

            return Ok("Booking Updated Successfuly");
        }

        [HttpGet("hotel/{id}")]
        public async Task<ActionResult<HotelBooking>> GetHotelBookingById(string id)
        {
            var dbhotel = await _context.HotelBookings.FindAsync(id);

            if (dbhotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            return Ok(dbhotel);
        }

        [HttpGet("hotel/overlap")]
        public ActionResult<int> CheckIfBookingOverlaps(DateTime checkIn, DateTime checkOut, int hotelId, string roomType)
        {
            var hotelOverlap = _context.HotelBookings.Where(x =>
                x.CheckIn <= checkOut &&
                x.CheckOut >= checkIn &&
                x.RoomType == roomType &&
                x.HotelId == hotelId
            ).ToList().Count();

            var packageOverlap = _context.PackageBookings.Where(x =>
                x.HotelCheckIn <= checkOut &&
                x.HotelCheckOut >= checkIn &&
                x.RoomType == roomType &&
                x.HotelId == hotelId
            ).ToList().Count();

            var overlap = hotelOverlap + packageOverlap;

            return Ok(overlap);
        }

        [HttpDelete("hotel/{id}")]
        public async Task<ActionResult<string>> DeleteHotelBooking(string id)
        {
            var dbHotel = await _context.HotelBookings.FindAsync(id);
            Payments payment = new Payments();
            var today = DateTime.Now;
            var datediff = dbHotel.CheckIn - today;

            if (dbHotel == null)
            {
                return NotFound("This hotel does not exist");
            }

            if (datediff.Days < 5)
            {
                return BadRequest("You cannot cancel this booking");
            }

            payment.UserId = dbHotel.UserId;
            payment.bookingId = dbHotel.Id;
            payment.bookingType = "Hotel";

            if (dbHotel.PaidInfull == true)
            {
                payment.AmountPaid = dbHotel.TotalPrice * -1;
            }
            else
            {
                payment.AmountPaid = dbHotel.DepositAmountPaid * -1;
            }

            payment.paymentType = "Refund";

            _context.Payments.Add(payment);

            _context.HotelBookings.Remove(dbHotel);
            await _context.SaveChangesAsync();

            return Ok("Booking Deleted");
        }

        [HttpPost("hotel/book")]
        public async Task<ActionResult<string>> AddHotelBooking(HotelBooking hotl)
        {

            Payments payment = new Payments();
            payment.UserId = hotl.UserId;
            payment.bookingId = hotl.Id;
            payment.bookingType = "Hotel";
            payment.AmountPaid = hotl.DepositAmountPaid;
            payment.paymentType = "Deposit";

            _context.Payments.Add(payment);

            _context.HotelBookings.Add(hotl);
            await _context.SaveChangesAsync();

            return Ok("Booking Successful");
        }





        [HttpGet("tour/userbooking")]
        public async Task<ActionResult<List<TourBookingViewModel>>> GetAllTourBookingsWithUserId(string userId)
        {
            var query = from tour in _context.Tours
                        join booking in _context.TourBookings on tour.Id equals booking.TourId
                        where booking.UserId == userId
                        select new TourBookingViewModel
                        {
                            bookingId = booking.Id,
                            TourName = tour.Name,
                            CommencementDate = booking.CommencementDate,
                            EndDate = booking.EndDate,
                            NumberOfGuests = booking.NumberOfPeople,
                            TotalPrice = booking.TotalPrice,
                            DepositAmountPaid = booking.DepositAmountPaid,
                            BookingDate = booking.BookingDate,
                            paidInfull = booking.PaidInfull,
                            IsCancelled = booking.IsCancelled,
                            PaymentDueDate = booking.PaymentDueDate
                        };
            var result = await query.ToListAsync();

            return Ok(result);
        }

        [HttpPut("tour/payment/{id}")]
        public async Task<ActionResult<string>> PayTourRemainder(string id)
        {
            var dbTour = await _context.TourBookings.FindAsync(id);
            Payments payment = new Payments();
            if (dbTour == null)
            {
                return NotFound("This tour does not exist");
            }

            if (dbTour.PaidInfull == true)
            {
                return BadRequest("You have already paid in full");
            }

            // dbTour.DepositAmountPaid = dbTour.TotalPrice;
            dbTour.PaidInfull = true;


            payment.UserId = dbTour.UserId;
            payment.bookingId = dbTour.Id;
            payment.bookingType = "Tour";
            payment.AmountPaid = dbTour.TotalPrice - dbTour.DepositAmountPaid;
            payment.paymentType = "PayRemainder";

            _context.Payments.Add(payment);

            _context.TourBookings.Update(dbTour);
            await _context.SaveChangesAsync();

            return Ok("Payment Successful");
        }


        [HttpPut("tour/{id}")]
        public async Task<ActionResult<string>> UpdateTour(string id, TourBooking tour, decimal surcharge)
        {
            var dbTour = await _context.TourBookings.FindAsync(id);
            Payments payment2 = new Payments();

            if (dbTour == null)
            {
                return NotFound("This tour does not exist");
            }

            dbTour.Id = tour.Id;
            dbTour.TourId = tour.TourId;
            dbTour.CommencementDate = tour.CommencementDate;
            dbTour.EndDate = tour.EndDate;
            dbTour.NumberOfPeople = tour.NumberOfPeople;
            dbTour.TotalPrice = tour.TotalPrice;
            dbTour.DepositAmountPaid = tour.DepositAmountPaid;
            dbTour.BookingDate = tour.BookingDate;
            dbTour.UserId = tour.UserId;
            dbTour.PaidInfull = tour.PaidInfull;
            dbTour.IsCancelled = tour.IsCancelled;
            dbTour.PaymentDueDate = tour.PaymentDueDate;


            payment2.UserId = dbTour.UserId;
            payment2.bookingId = dbTour.Id;
            payment2.bookingType = "Tour";
            payment2.AmountPaid = surcharge;
            payment2.paymentType = "Surcharge";

            _context.Payments.Add(payment2);

            _context.TourBookings.Update(dbTour);
            await _context.SaveChangesAsync();

            return Ok("Booking Updated Successfuly");
        }


        [HttpGet("tour/{id}")]
        public async Task<ActionResult<TourBooking>> GetTourBookingById(string id)
        {
            var dbTour = await _context.TourBookings.FindAsync(id);

            if (dbTour == null)
            {
                return NotFound("This Tour does not exist");
            }

            return Ok(dbTour);
        }

        //check if bookings overlap
        [HttpGet("tour/overlap")]
        public ActionResult<int> CheckIfTourBookingOverlaps(DateTime start, DateTime end, int tourId)
        {
            var guestCount = 0;
            List<TourBooking> tourOverlap = _context.TourBookings.Where(x =>
                x.CommencementDate == start &&
                x.TourId == tourId
            ).ToList();

            List<PackageBooking> packageOverlap = _context.PackageBookings.Where(x =>
                x.TourStartDate == start &&
                x.TourId == tourId
            ).ToList();


            foreach (var item in tourOverlap)
            {
                guestCount += item.NumberOfPeople;
            }

            foreach (var item in packageOverlap)
            {
                guestCount += item.NumberOfPeopleOnTour;
            }


            return Ok(guestCount);
        }

        [HttpDelete("tour/{id}")]
        public async Task<ActionResult<string>> DeleteTourBooking(string id)
        {
            var dbTour = await _context.TourBookings.FindAsync(id);
            var today = DateTime.Now;
            var datediff = dbTour.CommencementDate - today;
            Payments payment = new Payments();

            if (dbTour == null)
            {
                return NotFound("This tour booking does not exist");
            }

            if (datediff.Days < 5)
            {
                return BadRequest("You cannot cancel this booking");
            }


            payment.UserId = dbTour.UserId;
            payment.bookingId = dbTour.Id;
            payment.bookingType = "Tour";

            if (dbTour.PaidInfull == true)
            {
                payment.AmountPaid = dbTour.TotalPrice * -1;
            }
            else
            {
                payment.AmountPaid = dbTour.DepositAmountPaid * -1;
            }

            payment.paymentType = "Refund";

            _context.Payments.Add(payment);

            _context.TourBookings.Remove(dbTour);
            await _context.SaveChangesAsync();

            return Ok("Booking Deleted");
        }

        [HttpPost("tour/book")]
        public async Task<ActionResult<string>> AddTourBooking(TourBooking tour)
        {

            Payments payment = new Payments();
            payment.UserId = tour.UserId;
            payment.bookingId = tour.Id;
            payment.bookingType = "Tour";
            payment.AmountPaid = tour.DepositAmountPaid;
            payment.paymentType = "Deposit";

            _context.Payments.Add(payment);

            _context.TourBookings.Add(tour);
            await _context.SaveChangesAsync();

            return Ok("Booking Successful");
        }



        // Bookings for Packages

        [HttpPost("package/book")]
        public async Task<ActionResult<string>> AddPackageBooking(PackageBooking pkg)
        {

            Payments payment = new Payments();
            payment.UserId = pkg.UserId;
            payment.bookingId = pkg.Id;
            payment.bookingType = "Package";
            payment.AmountPaid = pkg.DepositAmountPaid;
            payment.paymentType = "Deposit";

            _context.Payments.Add(payment);


            _context.PackageBookings.Add(pkg);
            await _context.SaveChangesAsync();

            return Ok("Booking Successful");
        }

        [HttpGet("package/userbooking")]
        public async Task<ActionResult<List<TourBookingViewModel>>> GetAllPackageBookingsWithUserId(string userId)
        {
            var query =
                from booking in _context.PackageBookings
                join tour in _context.Tours on booking.TourId equals tour.Id
                join hotel in _context.Hotels on booking.HotelId equals hotel.Id
                where booking.UserId == userId
                select new PackageBookingViewModel
                {
                    bookingId = booking.Id,
                    TourName = tour.Name,
                    CommencementDate = booking.TourStartDate,
                    EndDate = booking.TourEndDate,
                    NumberOfGuests = booking.NumberOfPeopleOnTour,
                    hotelName = hotel.Name,
                    RoomType = booking.RoomType,
                    CheckIn = booking.HotelCheckIn,
                    CheckOut = booking.HotelCheckOut,
                    NumberOfNights = booking.NumberOfNights,
                    TotalPrice = booking.TotalPrice,
                    DepositAmountPaid = booking.DepositAmountPaid,
                    BookingDate = booking.BookingDate,
                    paidInfull = booking.PaidInfull,
                    IsCancelled = booking.IsCancelled,
                    PaymentDueDate = booking.PaymentDueDate
                };
            var result = await query.ToListAsync();

            return Ok(result);
        }

        [HttpPut("package/payment/{id}")]
        public async Task<ActionResult<string>> PayPackageRemainder(string id)
        {
            var dbPackage = await _context.PackageBookings.FindAsync(id);
            Payments payment = new Payments();

            if (dbPackage == null)
            {
                return NotFound("This bookings does not exist");
            }

            if (dbPackage.PaidInfull == true)
            {
                return BadRequest("You have already paid in full");
            }

            //dbPackage.DepositAmountPaid = dbPackage.TotalPrice;

            payment.UserId = dbPackage.UserId;
            payment.bookingId = dbPackage.Id;
            payment.bookingType = "Package";
            payment.AmountPaid = dbPackage.TotalPrice - dbPackage.DepositAmountPaid;
            payment.paymentType = "PayRemainder";

            _context.Payments.Add(payment);

            dbPackage.PaidInfull = true;

            _context.PackageBookings.Update(dbPackage);
            await _context.SaveChangesAsync();

            return Ok("Payment Successful");
        }

        [HttpPut("package/{id}")]
        public async Task<ActionResult<string>> UpdatePackage(string id, PackageBooking pkg, decimal paymentRemainder, decimal surcharge)
        {
            var dbPackage = await _context.PackageBookings.FindAsync(id);
            Payments payment = new Payments();
            Payments payment2 = new Payments();

            if (dbPackage == null)
            {
                return NotFound("This Package Booking does not exist");
            }

            dbPackage.Id = pkg.Id;
            dbPackage.TourId = pkg.TourId;
            dbPackage.TourStartDate = pkg.TourStartDate;
            dbPackage.TourEndDate = pkg.TourEndDate;
            dbPackage.NumberOfPeopleOnTour = pkg.NumberOfPeopleOnTour;
            dbPackage.HotelId = pkg.HotelId;
            dbPackage.RoomType = pkg.RoomType;
            dbPackage.HotelCheckIn = pkg.HotelCheckIn;
            dbPackage.HotelCheckOut = pkg.HotelCheckOut;
            dbPackage.NumberOfNights = pkg.NumberOfNights;
            dbPackage.TotalPrice = pkg.TotalPrice;
            dbPackage.DepositAmountPaid = pkg.DepositAmountPaid;
            dbPackage.BookingDate = pkg.BookingDate;
            dbPackage.UserId = pkg.UserId;
            dbPackage.PaidInfull = pkg.PaidInfull;
            dbPackage.IsCancelled = pkg.IsCancelled;
            dbPackage.PaymentDueDate = pkg.PaymentDueDate;


            if (paymentRemainder != 0)
            {
                payment.UserId = dbPackage.UserId;
                payment.bookingId = dbPackage.Id;
                payment.bookingType = "Package";
                payment.AmountPaid = paymentRemainder;
                payment.paymentType = "ModifyBooking";
                _context.Payments.Add(payment);
            }


            payment2.UserId = dbPackage.UserId;
            payment2.bookingId = dbPackage.Id;
            payment2.bookingType = "Package";
            payment2.AmountPaid = surcharge;
            payment2.paymentType = "Surcharge";

            _context.Payments.Add(payment2);




            _context.PackageBookings.Update(dbPackage);
            await _context.SaveChangesAsync();

            return Ok("Booking Updated Successfuly");
        }


        [HttpGet("package/{id}")]
        public async Task<ActionResult<PackageBooking>> GetPackageBookingById(string id)
        {
            var dbPackage = await _context.PackageBookings.FindAsync(id);

            if (dbPackage == null)
            {
                return NotFound("This booking does not exist");
            }

            return Ok(dbPackage);
        }

        [HttpDelete("package/{id}")]
        public async Task<ActionResult<string>> DeletePackageBooking(string id)
        {
            var dbPackage = await _context.PackageBookings.FindAsync(id);
            var today = DateTime.Now;
            var tourdatediff = dbPackage.TourStartDate - today;
            var hoteldatediff = dbPackage.HotelCheckIn - today;
            Payments payment = new Payments();

            if (dbPackage == null)
            {
                return NotFound("This tour booking does not exist");
            }

            if (tourdatediff.Days < 5 || hoteldatediff.Days < 5)
            {
                return BadRequest("You cannot cancel this booking");
            }

            payment.UserId = dbPackage.UserId;
            payment.bookingId = dbPackage.Id;
            payment.bookingType = "Package";

            if (dbPackage.PaidInfull == true)
            {
                payment.AmountPaid = dbPackage.TotalPrice * -1;
            }
            else
            {
                payment.AmountPaid = dbPackage.DepositAmountPaid * -1;
            }

            payment.paymentType = "Refund";

            _context.Payments.Add(payment);



            _context.PackageBookings.Remove(dbPackage);
            await _context.SaveChangesAsync();

            return Ok("Booking Deleted");
        }
    }
}
