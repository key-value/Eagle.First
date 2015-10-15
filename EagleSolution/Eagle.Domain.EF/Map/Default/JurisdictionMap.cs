using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class JurisdictionMap : EntityTypeConfiguration<Jurisdiction>
    {
        public JurisdictionMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}