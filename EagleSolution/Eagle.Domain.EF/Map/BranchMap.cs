using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public BranchMap()
        {
            this.HasKey(x => x.ID);
            this.Ignore(x => x.Branches);
            this.Ignore(x => x.ActionButtons);

        }
    }
}