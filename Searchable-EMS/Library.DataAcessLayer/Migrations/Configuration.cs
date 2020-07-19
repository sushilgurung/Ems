namespace Library.DataAcessLayer.Migrations
{
    using Library.DataAcessLayer.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.DataAcessLayer.Context.DatabaseEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Library.DataAcessLayer.Context.DatabaseEntities context)
        {
           // DataInitializer data = new DataInitializer(context);
            DataInitializer.SeedData(context);
            //context.Role.AddOrUpdate(
            //    new Roles() { RoleId = Guid.NewGuid(), RoleName = "anonymous", RoleFriendlyName = "anonymous" }
            //    );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
