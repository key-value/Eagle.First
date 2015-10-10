﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using Eagle.Domain.Core.Model;
using Eagle.Domain.EF.Map;
using Eagle.Model;

namespace Eagle.Domain.EF.DataContext
{
    public partial class DefaultContext : DbContext
    {
        static DefaultContext()
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultContext, Domain.EF.Migrations.Configuration>());
            //Database.SetInitializer<DefaultContext>(null);

#if DEBUG
            DbInterception.Add(new EFIntercepterLogging());
#endif
        }
        public DefaultContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.UseDatabaseNullSemantics = false;
        }
        public void ModifiedModel(IEntity entity)
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        #region Overrides of DbContext

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        ///             before the model has been locked down and used to initialize the context.  The default
        ///             implementation of this method does nothing, but it can be overridden in a derived class
        ///             such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        ///             is created.  The model for that context is then cached and is for all further instances of
        ///             the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///             property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///             More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///             classes directly.
        /// </remarks>
        /// <param name="modelBuilder">The builder that defines the model for the context being created. </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(AccountMap)));
        }


        #endregion


        public DbSet<Letter> Letters
        {
            get; set;
        }

        public DbSet<Warehouse> Warehouses
        {
            get; set;
        }

        public DbSet<SystemCard> SystemCards
        {
            get; set;
        }

    }
}