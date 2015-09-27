using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using Eagle.Domain.EF.DataContext;
using Eagle.Model;

namespace Eagle.Domain.EF
{
    public class AccountContext : DefaultContext
    {
        static AccountContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultContext, Migrations.Configuration>());
        }

        /// <summary>
        /// 页面分配表
        /// </summary>
        public DbSet<Jurisdiction> Jurisdictions { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Branch> Branches { get; set; }
    }
}