using System.Data.Entity;
using Eagle.Domain.EF.DataContext;
using Eagle.Model;

namespace Eagle.Domain.EF.DataContext
{
    public partial class DefaultContext
    {
        public DbSet<MonitorRestaurant> MonitorRestaurants
        {
            get; set;
        }

        public DbSet<RestaurantState> RestaurantStates
        {
            get; set;
        }

        public DbSet<MonitorCity> MonitorCities
        {
            get; set;
        }

        public DbSet<MonitorTable> MonitorTables
        {
            get; set;
        }

        public DbSet<MonitorConnectStep> MonitorConnectSteps
        {
            get; set;
        }

        public DbSet<RestPace> RestPaces
        {
            get; set;
        }
    }
}