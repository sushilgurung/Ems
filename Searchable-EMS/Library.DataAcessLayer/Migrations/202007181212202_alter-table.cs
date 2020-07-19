namespace Library.DataAcessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "Designation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "Designation", c => c.Int(nullable: false));
        }
    }
}
