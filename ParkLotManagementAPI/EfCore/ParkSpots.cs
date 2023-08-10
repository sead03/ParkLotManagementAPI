using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkLotManagementAPI.EfCore
{
    [Table("parkSpots")]
    public class ParkSpots
    {      
        [Key, Required]
        public int ParkSpotsid { get; set; }
        [Required]
        public int? reservedSpots { get; set; }
        [Required]
        public int freeSpots { get; set;}
        [Required]
        public int totalSpots { get; set; }

    }
}
