using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2")]
        public decimal Cost { get; set; }
        public int DurationInDays { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public string? Description { get; set; }

    }
}
