using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class MonitorRestaurantMap : EntityTypeConfiguration<MonitorRestaurant>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public MonitorRestaurantMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}