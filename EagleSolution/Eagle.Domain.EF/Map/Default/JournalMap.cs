using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class JournalMap : EntityTypeConfiguration<Journal>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public JournalMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}