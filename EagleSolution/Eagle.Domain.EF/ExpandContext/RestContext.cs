using System.Data.Entity;
using Eagle.Domain.EF.DataContext;
using Eagle.Model;

namespace Eagle.Domain.EF
{
    public class RestContext : DefaultContext
    {
        public DbSet<MonitorRestaurant> MonitorRestaurants { get; set; }

        public DbSet<RestaurantState> RestaurantStates { get; set; }

        public DbSet<MonitorCity> MonitorCities { get; set; }

        public DbSet<MonitorTable> MonitorTables { get; set; }
    }
}