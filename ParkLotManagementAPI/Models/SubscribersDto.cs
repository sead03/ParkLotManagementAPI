namespace ParkLotManagementAPI.Models
{
    public class GetSubscribers
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int cardNumberId { get; set; }
        public string email { get; set; }
        public int phoneNumber { get; set; }
        public DateOnly birthday { get; set; }
        public string plateNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
