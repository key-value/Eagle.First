using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class WeekTargetMap : EntityTypeConfiguration<WeekTarget>
    {
        public WeekTargetMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}