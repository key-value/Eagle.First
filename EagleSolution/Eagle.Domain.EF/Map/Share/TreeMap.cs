
using System.Data.Entity.ModelConfiguration;
using Eagle.Model;
using Eagle.Model.Share;

namespace Eagle.Domain.EF.Map.Share
{
    public class TreeMap : EntityTypeConfiguration<ShareTree>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public TreeMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}