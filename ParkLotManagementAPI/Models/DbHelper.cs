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
                ParkSpotsid = row.ParkSpotsid,
                reservedSpots = row.reservedSpots,
                freeSpots = row.freeSpots,
                totalSpots = row.totalSpots
            }));
            return response;
        }
        public void SaveParkSpots (ParkSpotsDto parkSpots)
        {
            ParkSpots dbTable = new ParkSpots();
            if (parkSpots.id > 0)
            {
                //PUT
                dbTable = _context.parkSpots.Where(d => d.ParkSpotsid.Equals(parkSpots.id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.reservedSpots = parkSpots.reservedSpots;
                    dbTable.freeSpots = parkSpots.freeSpots;
                }
                _context.SaveChanges();
            }
        }

    }
}
        
