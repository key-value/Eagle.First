using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class HeartbeatMap : EntityTypeConfiguration<Heartbeat>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public HeartbeatMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}