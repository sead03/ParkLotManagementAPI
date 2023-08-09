using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkLotManagementAPI.EfCore
{
    [Table("dailylogs")]
    public class DailyLogs
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int code { get; set; }
        [Required]
        public int subscriptionId { get; set; }
        [Required]
        public TimeOnly checkIn { get; set; }
        [Required]
        public TimeOnly checkOut { get; set; }
        [Required]
        public int price { get; set; }
        public virtual ICollection<DailyLogs> dailylogs 
        {
            get;
            set;
        }
    }
}
