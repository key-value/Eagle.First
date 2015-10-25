using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class TrackTargetMap : EntityTypeConfiguration<TrackTarget>
    {
        public TrackTargetMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}