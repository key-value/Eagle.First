using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class WorkRecordMap : EntityTypeConfiguration<WorkRecord>
    {
        public WorkRecordMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}