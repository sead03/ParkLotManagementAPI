namespace ParkLotManagementAPI.Models { 
    public class ParkSpotsDto
    {
        public int id {  get; set; }
        public int reservedSpots { get; set; }
        public int freeSpots { get; set; }
        public int totalSpots { get; set; }
        public ParkSpotsDto() 
        {
            totalSpots = reservedSpots + freeSpots;
        }
    }
}
