using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared.Models
{
    public class HotelBookingViewModel
    {
        public string bookingId { get; set; }
        public string hotelName { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckIn { get; set; } 
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositAmountPaid { get; set; }
        public DateTime BookingDate { get; set; } 
        public bool paidInfull { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime PaymentDueDate { get; set; }


        

    }
}
