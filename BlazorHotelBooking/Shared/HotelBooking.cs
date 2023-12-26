using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared
{
    public class HotelBooking
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Room Type required.")]
        public string RoomType { get; set; }
        [Required(ErrorMessage = "Check-In Date required.")]
        public DateOnly CheckIn { get; set; } = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1));
        public DateOnly CheckOut { get; set; }
        [Required(ErrorMessage = "Number of Nigts required.")]
        public int NumberOfNights { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal DepositAmountPaid { get; set; }
        public string UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
    }
}
