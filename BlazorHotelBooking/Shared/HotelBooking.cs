﻿using System;
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
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Room Type required.")]
        public string RoomType { get; set; }
        [Required(ErrorMessage = "Check-In Date required.")]
        public DateTime CheckIn { get; set; } = DateTime.Now.Date.AddDays(1);
        public DateTime CheckOut { get; set; }
        [Required(ErrorMessage = "Number of Nigts required.")]
        public int NumberOfNights { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal DepositAmountPaid { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now.Date;
        public bool paidInfull { get; set; } = false;
        // public string? UserPhone { get; set; }
    }
}
