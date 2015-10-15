using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class RestPaceMap : EntityTypeConfiguration<RestPace>
    {
        public RestPaceMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}