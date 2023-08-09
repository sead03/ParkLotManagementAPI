using ParkLotManagementAPI.EfCore;

namespace ParkLotManagementAPI.Models
{
    public class DbHelper
    {
        private EF_DataContext _context;
        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public List<ParkSpots> GetParkSpots()
        {
            List<ParkSpots> response = new List<ParkSpots>();
            var dataList = _context.parkSpots.ToList();
            dataList.ForEach(row => response.Add(new ParkSpots()
            {
                id = row.id,
                reservedSpots = row.reservedSpots,
                freeSpots = row.freeSpots,
                totalSpots = row.totalSpots
            }));
            return response;
        }
    }
}
        
