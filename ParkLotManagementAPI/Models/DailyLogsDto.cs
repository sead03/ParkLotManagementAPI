namespace ParkLotManagementAPI.Models
{
    public class DailyLogsDto
    {
        public int Id { get; set; }
        public int code { get; set; } 
        public int subscriptionId { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public int price { get; set; }
    }
}
