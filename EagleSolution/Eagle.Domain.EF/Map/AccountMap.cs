using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        /// <summary>
        /// Initializes a new instance of EntityTypeConfiguration
        /// </summary>
        public AccountMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}