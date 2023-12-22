using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHotelBooking.Shared
{
    public class Hotel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public float SBPrice { get; set; }
        [Required]
        public float DBPrice { get; set; }
        [Required]
        public float FamPrice { get; set; }
        [Required]
        public int Spaces { get; set; }
        public string Description { get; set; }
    }
}
