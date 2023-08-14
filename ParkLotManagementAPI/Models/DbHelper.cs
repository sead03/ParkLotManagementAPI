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
        /// <returns></returns>s
        /// 
        public List<ParkSpots> GetParkSpots()
        {
            List<ParkSpots> response = new List<ParkSpots>();
            var dataList = _context.parkSpots.ToList();
            var members = _context.subscribers.ToList();
            dataList.ForEach(row => response.Add(new ParkSpots()
            {
                ParkSpotsid = row.ParkSpotsid,
                freeSpots = row.totalSpots - members.Where(x=>x.isDeleted == false).Count(),
                reservedSpots = members.Where(x=>x.isDeleted==false).Count(),
                totalSpots = row.totalSpots
            }));
            return response;
        }
        public List<WeekdayPricePlan> GetWeekdayPricePlans()
        {
            List<WeekdayPricePlan> response = new List<WeekdayPricePlan>();
            var dataList = _context.weekdayPricePlans.ToList();
            dataList.ForEach(row => response.Add(new WeekdayPricePlan()
            {
                Id = row.Id,
                hourlyPrice = row.hourlyPrice,
                dailyPrice = row.dailyPrice,
                minimumHours = row.minimumHours
            }));
            return response;
        }
        public List<WeekendPricePlan> GetWeekendPricePlans()
        {
            List<WeekendPricePlan> response = new List<WeekendPricePlan>();
            var dataList = _context.weekendPricePlans.ToList();
            dataList.ForEach(row => response.Add(new WeekendPricePlan()
            {
                Id = row.Id,
                hourlyPrice = row.hourlyPrice,
                dailyPrice = row.dailyPrice,
                minimumHours = row.minimumHours
            }));
            return response;
        }
        public List<Subscribers> GetSubscriber()
        {
            List<Subscribers> response = new List<Subscribers>();
            var dataList = _context.subscribers.ToList();
            dataList.ForEach(row => response.Add(new Subscribers()
            {
                id = row.id,
                firstName = row.firstName,
                lastName = row.lastName,
                cardNumberId = row.cardNumberId,
                email = row.email,
                phoneNumber = row.phoneNumber,
                birthday = row.birthday,
                plateNumber = row.plateNumber,
            }));
            return response;
        }
        public List<Subscriptions> GetSubscriptions()
        {
            List<Subscriptions> response = new List<Subscriptions>();
            var dataList = _context.subscriptions.Where(x=>x.isDeleted==false).ToList();
            dataList.ForEach(row => response.Add(new Subscriptions()
            {
                id = row.id,
                code = row.code,
                subscriberId = row.subscriberId,
                price = row.price,
                startDate = row.startDate,
                endDate = row.endDate,
            }));
            return response;
        }

        /// <summary>
        /// It serves the POST/PUT/PATCH
        /// </summary>
        /// 
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
        
        public void SaveWeekdayPricePlans(WeekdayPricePlanDto weekdayPricePlan)
        {
            WeekdayPricePlan dbTable = new WeekdayPricePlan();
            if (weekdayPricePlan.Id > 0)
            {
                //PUT
                dbTable = _context.weekdayPricePlans.Where(d => d.Id.Equals(weekdayPricePlan.Id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.hourlyPrice = weekdayPricePlan.hourlyPrice;
                    dbTable.dailyPrice = weekdayPricePlan.dailyPrice;
                    dbTable.minimumHours = weekdayPricePlan.minimumHours;
                }
                _context.SaveChanges();
            }
        }
        
        public void SaveWeekendPricePlans(WeekendPricePlanDto weekendPricePlan)
        {
            WeekendPricePlan dbTable = new WeekendPricePlan();
            if (weekendPricePlan.Id > 0)
            {
                //PUT
                dbTable = _context.weekendPricePlans.Where(d => d.Id.Equals(weekendPricePlan.Id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.hourlyPrice = weekendPricePlan.hourlyPrice;
                    dbTable.dailyPrice = weekendPricePlan.dailyPrice;
                    dbTable.minimumHours = weekendPricePlan.minimumHours;
                }
                _context.SaveChanges();
            }
        }
        public void PostSubscriber(Subscribers subscribers)
        {
            Subscribers dbTable = new Subscribers();

                dbTable.id = subscribers.id;
                dbTable.firstName = subscribers.firstName;
                dbTable.lastName = subscribers.lastName;
                dbTable.cardNumberId = subscribers.cardNumberId;
                dbTable.email = subscribers.email;
                dbTable.phoneNumber = subscribers.phoneNumber;
                dbTable.birthday = subscribers.birthday;
                dbTable.plateNumber = subscribers.plateNumber;
                _context.subscribers.Add(dbTable);

            _context.SaveChanges();
        }
        public void UpdateSubscription(Subscriptions subscriptions)
        {
            Subscriptions dbTable = new Subscriptions();
            if (subscriptions.id > 0)
            {
                //PUT
                dbTable = _context.subscriptions.Where(d => d.id.Equals(subscriptions.id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.id = subscriptions.id;
                    dbTable.code = subscriptions.code;
                    dbTable.subscriberId = subscriptions.subscriberId;
                    dbTable.price = subscriptions.price;
                    dbTable.startDate = subscriptions.startDate;
                    dbTable.endDate = subscriptions.endDate;    

                }
            }
            else
            {
                //POST
                dbTable.id = subscriptions.id;
                dbTable.code = subscriptions.code;
                dbTable.subscriberId = subscriptions.subscriberId;
                dbTable.price = subscriptions.price;
                dbTable.startDate = subscriptions.startDate;
                dbTable.endDate = subscriptions.endDate;
                _context.subscriptions.Add(dbTable);
            }
            _context.SaveChanges();
        }

        public void PostDailyLog(DailyLogs dailyLogs)
        {
            DailyLogs dbTable = new DailyLogs();
            if (dailyLogs.Id > 0)
            {
                //POST
                dbTable = _context.dailyLogs.Where(d => d.Id.Equals(dailyLogs.Id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.Id = dailyLogs.Id;
                    dbTable.code = dailyLogs.code;
                    dbTable.subscriptionId = dailyLogs.subscriptionId;
                    dbTable.checkIn = dailyLogs.checkIn;
                    dbTable.checkOut = dailyLogs.checkOut;
                    dbTable.price = dailyLogs.price;
                }
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSubscriber(int id)
        {
            var subscriber = _context.subscribers.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (subscriber != null)
            {
                _context.subscribers.Remove(subscriber);
                _context.SaveChanges();
            }
        }
        public void DeleteSubscription(int id)
        {
            var subscription = _context.subscriptions.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (subscription != null)
            {
                subscription.isDeleted =true;
                _context.SaveChanges();
            }
        }
    }
}
        
