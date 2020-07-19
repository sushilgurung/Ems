namespace Library.DataAcessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class IsImported : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "IsImported", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Employee", "ImportedDate", c => c.DateTime());
        }

        public override void Down()
        {
            DropColumn("dbo.Employee", "ImportedDate");
            DropColumn("dbo.Employee", "IsImported");
        }
    }
}
