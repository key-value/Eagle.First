using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class RestaurantStateMap : EntityTypeConfiguration<RestaurantState>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public RestaurantStateMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}