using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class WeekSummaryMap : EntityTypeConfiguration<WeekSummary>
    {
        public WeekSummaryMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}