using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class TrackRecordMap : EntityTypeConfiguration<TrackRecord>
    {
        public TrackRecordMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}