using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared
{
    internal class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2")]
        public decimal Cost { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public int DurationInDays { get; set; }
        public string? Description { get; set; }

    }
}
