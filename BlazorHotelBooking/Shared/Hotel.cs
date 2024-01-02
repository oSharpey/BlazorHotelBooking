using System.ComponentModel.DataAnnotations.Schema;

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
        public string? Description { get; set; }
    }
}
