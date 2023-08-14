using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkLotManagementAPI.EfCore
{
    [Table("subscribers")]
    public class Subscribers
    {
        [Key, Required]
        public int id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public int cardNumberId { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public int phoneNumber { get; set; }
        [Required]
        public DateOnly birthday { get; set; }
        [Required]
        public string plateNumber { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
