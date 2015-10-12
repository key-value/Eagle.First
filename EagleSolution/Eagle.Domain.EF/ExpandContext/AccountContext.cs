using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using Eagle.Domain.EF.DataContext;
using Eagle.Model;

namespace Eagle.Domain.EF.DataContext
{
    public partial class DefaultContext
    {

        /// <summary>
        /// 页面分配表
        /// </summary>
        public DbSet<Jurisdiction> Jurisdictions
        {
            get; set;
        }

        public DbSet<Account> Accounts
        {
            get; set;
        }

        public DbSet<Branch> Branches
        {
            get; set;
        }

        public DbSet<WorkCard> WorkCards
        {
            get; set;
        }

        public DbSet<Department> Departments
        {
            get; set;
        }
    }
}