using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorHotelBooking.Shared
{
    public class PackageBooking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public DateTime HotelCheckIn { get; set; } = DateTime.Now.Date.AddMonths(2);
        public DateTime HotelCheckOut { get; set; }
        public int NumberOfNights { get; set; }
        public int TourId { get; set; }
        [Required(ErrorMessage = "Commencement Date required.")]
        public DateTime TourStartDate { get; set; } = DateTime.Now.Date.AddMonths(2);
        public DateTime TourEndDate { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal TotalPrice { get; set; }
        public int NumberOfPeopleOnTour { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal DepositAmountPaid { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now.Date;
        public bool PaidInfull { get; set; } = false;
        public bool IsCancelled { get; set; } = false;
        public DateTime PaymentDueDate { get; set; }


    }
}
