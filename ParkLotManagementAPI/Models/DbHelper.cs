using Microsoft.AspNetCore.Mvc.Rendering;
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
                isDeleted = row.isDeleted,
            }));
            return response;
        }
        public bool CheckDuplicateIdCardNumber(int cardNumberId)
        {
            return _context.subscribers.Any(subscriber => subscriber.cardNumberId == cardNumberId);
        }
        public List<Subscriptions> GetSubscription()
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
        public Subscriptions GetSubscriptionByCode(int code)
        {
            return _context.subscriptions.FirstOrDefault(subscription => subscription.code == code);
        }

        public List<DailyLogs> GetLogsWithCalculatedPrice()
        {
            var logs = _context.dailyLogs.ToList();
            var weekdayPricePlan = _context.weekdayPricePlans.OrderByDescending(i => i.hourlyPrice).FirstOrDefault();
            var weekendPricePlan = _context.weekendPricePlans.OrderByDescending(i => i.hourlyPrice).FirstOrDefault();
            var subscription = _context.subscriptions.OrderByDescending(i => i.subscriberId).FirstOrDefault();

            List<DailyLogs> logsWithCalculatedPrice = new List<DailyLogs>();

            foreach (var log in logs)
            {
                int calculatedPrice = CalculatePricePlan(log, weekdayPricePlan, weekendPricePlan, subscription);

                logsWithCalculatedPrice.Add(new DailyLogs
                {
                    Id = log.Id,
                    code = log.code,
                    subscriptionId = log.subscriptionId,
                    checkIn = log.checkIn,
                    checkOut = log.checkOut,
                    price = calculatedPrice
                });
            }
            return logsWithCalculatedPrice;
        }

        public int CalculatePricePlan(DailyLogs dailyLogs,WeekdayPricePlan weekdayPricePlan, WeekendPricePlan weekendPricePlan,Subscriptions subscriptions)
        {
            // Step 1: Check if the check-in has happened on a weekend
            bool isWeekend = dailyLogs.checkIn.DayOfWeek == DayOfWeek.Saturday || dailyLogs.checkIn.DayOfWeek == DayOfWeek.Sunday;
            // Step 2: Calculate the total number of hours
            TimeSpan duration = dailyLogs.checkOut - dailyLogs.checkIn;
            double totalHours = duration.TotalHours;
            // Step 3: Minimum hours of the payment plan
            int minimumHours = isWeekend ? weekdayPricePlan.minimumHours : weekendPricePlan.minimumHours;
            // Step 4/1 and 4/2: Calculate the price
            int hourlyRate = isWeekend ? weekdayPricePlan.hourlyPrice : weekendPricePlan.hourlyPrice; // Adjust hourly rates for weekday and weekend
            int dailyRate = isWeekend ? weekdayPricePlan.dailyPrice : weekendPricePlan.dailyPrice;

            int price;

            if (dailyLogs.subscriptionId == subscriptions.subscriberId)
            {
                price = 0; // Subscription ID is present, price is 0
            }
            else if (totalHours <= minimumHours)
            {
                price = hourlyRate * (int)totalHours;
            }
            else
            {
                int numberOfDays = (int)Math.Floor(totalHours / 24);
                double remainingHours = totalHours % 24;

                if (remainingHours <= minimumHours)
                {
                    price = (int)numberOfDays * dailyRate + hourlyRate * (int)remainingHours;
                }
                else
                {
                    price = (int)(numberOfDays + 1) * dailyRate;
                }
            }

            dailyLogs.price = price;
            
            return price;
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

        public void UpdateSubscriber(Subscribers subscribers)
        {
            Subscribers dbTable = _context.subscribers.FirstOrDefault(d => d.id == subscribers.id);
            if (dbTable != null)
            {
                dbTable.firstName = subscribers.firstName;
                dbTable.lastName = subscribers.lastName;
                dbTable.cardNumberId = subscribers.cardNumberId;
                dbTable.email = subscribers.email;
                dbTable.phoneNumber = subscribers.phoneNumber;
                dbTable.birthday = subscribers.birthday;
                dbTable.plateNumber = subscribers.plateNumber;

                _context.SaveChanges();
            }
        }
        public void PostSubscriber(Subscribers subscribers)
        {
            Subscribers dbTable = new Subscribers();

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
        public void CreateSubscription(Subscriptions subscription)
        {
            // Check for duplicate subscription code
            bool isDuplicateCode = _context.subscriptions.Any(s => s.code == subscription.code);
            if (isDuplicateCode)
            {
                throw new Exception("A subscription with the same code already exists.");
            }

            _context.subscriptions.Add(subscription);
            _context.SaveChanges();
        }
        public void UpdateSubscription(Subscriptions subscription)
        {
            Subscriptions dbtable = _context.subscriptions.FirstOrDefault(s => s.id == subscription.id);

            if (dbtable != null)
            {
                dbtable.price = subscription.price;
                dbtable.startDate = subscription.startDate;
                dbtable.endDate = subscription.endDate;

                _context.SaveChanges();
            }
        }
        private int GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999); // Generate a random 4-digit code
        }

        public void CreateLogWithRandomCode(DailyLogs dailyLogs)
        {

            // Create a new log with the generated random code
            DailyLogs newLog = new DailyLogs
            {
                code = randomCode,
                subscriptionId = dailyLogs.subscriptionId,
                checkIn = dailyLogs.checkIn,
                checkOut = dailyLogs.checkOut,
                price = dailyLogs.price
            };

            _context.dailyLogs.Add(newLog);
            _context.SaveChanges();
        }

       
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSubscriber(int id)
        {
            Subscribers dbTable = _context.subscribers.FirstOrDefault(d => d.id == id);
            if (dbTable != null)
            {
                dbTable.isDeleted = true;
            }
            _context.SaveChanges();

        }
        public void DeleteSubscription(int id)
        {
            Subscriptions dbTable = _context.subscriptions.FirstOrDefault(d => d.id == id);
            if (dbTable != null)
            {
                dbTable.isDeleted = true;
            }
            _context.SaveChanges();

        }
    }
}
        
