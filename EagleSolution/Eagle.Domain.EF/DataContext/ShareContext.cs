using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using Eagle.Domain.Core.Model;
using Eagle.Domain.EF.Map.Share;
using Eagle.Domain.EF.Migrations.Share;
using Eagle.Model.Share;

namespace Eagle.Domain.EF.DataContext
{
    public class ShareContext : DbContext
    {
        static ShareContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShareContext, Configuration>());
            //Database.SetInitializer<DefaultContext>(null);

#if DEBUG
            DbInterception.Add(new EFIntercepterLogging());
#endif
        }

        public ShareContext()
            : base("ShareConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            //this.Configuration.UseDatabaseNullSemantics = false;
        }

        public DbSet<ShareTree> Trees { get; set; }

        public void ModifiedModel(IEntity entity)
        {
            Entry(entity).State = EntityState.Modified;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof (TreeMap)));
        }
    }
}