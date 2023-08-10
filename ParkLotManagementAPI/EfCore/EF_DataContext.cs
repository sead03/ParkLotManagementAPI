using Microsoft.EntityFrameworkCore;

namespace ParkLotManagementAPI.EfCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<ParkSpots> parkSpots { get; set; }
        public DbSet<Subscribers> subscribers { get; set; }
        public DbSet<Subscriptions> subscriptions { get; set; }
        public DbSet<WeekdayPricePlan> weekdayPricePlans { get; set;}
        public DbSet<WeekendPricePlan> weekendPricePlans { get; set; }
        public DbSet<DailyLogs> dailyLogs { get; set; }
    }
}

