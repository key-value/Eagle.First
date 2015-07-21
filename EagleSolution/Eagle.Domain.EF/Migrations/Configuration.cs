using Eagle.Model;

namespace Eagle.Domain.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Eagle.Domain.EF.DataContext.DefaultContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Eagle.Domain.EF.DataContext.DefaultContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /*context.Branches.AddOrUpdate(
               x => x.ID,
                new Branch[]
                {
                    new Branch(){ ID = new Guid("C9C09D20-0A65-45C9-B641-8B5AD0369326"),SortID = 1,Title = "用户管理",Enble = true,AreaName = "Manage",Logo = "linecons-cog",Level = 1,},
                    new Branch(){ID = new Guid("E412E20A-94B3-4662-BB7E-E3E22E91478A"),SortID = 2,Title = "账号管理",PreBranch = new Guid("C9C09D20-0A65-45C9-B641-8B5AD0369326"),Enble = true,AreaName = "Manage",ActionName="Branch",Level = 2,},
                    new Branch(){ID = new Guid("42F76BC7-EE25-4566-9111-9801CDAD9A46"),SortID = 3,Title = "菜单管理",PreBranch = new Guid("C9C09D20-0A65-45C9-B641-8B5AD0369326"),Enble = true,AreaName = "Manage",ActionName="Branch",Level = 2,},
                    new Branch(){ID = new Guid("4000E9A2-833E-44CB-A209-D5FA43E76638"),SortID = 4,Title = "权限管理",PreBranch = new Guid("C9C09D20-0A65-45C9-B641-8B5AD0369326"),Enble = true,AreaName = "Manage",ActionName="Branch",Level = 2,},
                    new Branch(){ID = new Guid("1B932341-04ED-45E5-AFC9-40B108CFDD8D"),SortID = 5,Title = "权限分配",PreBranch = new Guid("C9C09D20-0A65-45C9-B641-8B5AD0369326"),Enble = true,AreaName = "Manage",ActionName="Branch",Level = 2,},
                    new Branch(){ ID = new Guid("50C02C55-A652-403D-90BF-3BC1194B27C1"),SortID = 6,Title = "门店管理",Enble = true,AreaName = "Rest",Logo = "linecons-cog",Level = 1,},
                }

            );
*/

        }
    }
}
