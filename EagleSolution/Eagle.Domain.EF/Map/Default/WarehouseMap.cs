using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public WarehouseMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}