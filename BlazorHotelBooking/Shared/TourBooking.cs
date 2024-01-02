using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorHotelBooking.Shared
{
    public class TourBooking
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int TourId { get; set; }
        [Required(ErrorMessage = "Commencement Date required.")]
        public DateTime CommencementDate { get; set; } = DateTime.Now.Date.AddMonths(2);
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal DepositAmountPaid { get; set; }
        public int NumberOfPeople { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now.Date;
        public bool PaidInfull { get; set; } = false;
        public bool IsCancelled { get; set; } = false;
        public DateTime PaymentDueDate { get; set; }
    }
}
