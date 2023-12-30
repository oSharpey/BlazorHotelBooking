﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared.Models
{
    public class TourBookingViewModel
    {
        public string bookingId { get; set; }
        public string TourName { get; set; }
        public DateTime CommencementDate{ get; set; } 
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DepositAmountPaid { get; set; }
        public DateTime BookingDate { get; set; } 
        public bool paidInfull { get; set; }
        public bool IsCancelled { get; set; }


        

    }
}