using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkLotManagementAPI.EfCore
{
    [Table("weekdaypriceplan")]
    public class WeekdayPricePlan
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int hourlyPrice { get; set; }
        [Required]
        public int dailyPrice { get; set; }
        [Required]
        public int minimumHours { get; set; }
        public virtual ICollection<WeekdayPricePlan> weekdaypriceplans 
        {
            get;
            set;
        }
    }
}
