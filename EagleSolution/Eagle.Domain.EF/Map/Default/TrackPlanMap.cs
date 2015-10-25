using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class TrackPlanMap : EntityTypeConfiguration<TrackPlan>
    {
        public TrackPlanMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}