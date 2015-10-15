using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class MonitorCityMap : EntityTypeConfiguration<MonitorCity>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public MonitorCityMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}