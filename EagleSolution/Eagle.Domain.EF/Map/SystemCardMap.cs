using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map
{
    public class SystemCardMap : EntityTypeConfiguration<SystemCard>
    {
        public SystemCardMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}