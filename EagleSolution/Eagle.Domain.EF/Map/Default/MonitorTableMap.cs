using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class MonitorTableMap : EntityTypeConfiguration<MonitorTable>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public MonitorTableMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}