using Microsoft.Kiota.Abstractions;
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
        public DateTime checkIn { get; set; }
        [Required]
        public DateTime checkOut { get; set; }
        [Required]
        public int price { get; set; }

    }
}
