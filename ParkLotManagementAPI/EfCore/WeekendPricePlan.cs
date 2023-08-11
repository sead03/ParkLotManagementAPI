using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkLotManagementAPI.EfCore
{
    [Table("weekendpriceplan")]
    public class WeekendPricePlan
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int hourlyPrice { get; set; }
        [Required]
        public int dailyPrice { get; set; }
        [Required]
        public int minimumHours { get; set; }

    }
}
