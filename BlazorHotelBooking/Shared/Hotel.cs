using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2")]
        public decimal SBPrice { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal DBPrice { get; set; }
        [Column(TypeName = "decimal(18,2")]
        public decimal FamPrice { get; set; }
        public int Spaces { get; set; }
        public string? Description { get; set; }
    }
}
