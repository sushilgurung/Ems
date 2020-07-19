namespace Library.DataAcessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "UpdatedDateBy", c => c.Guid());
            AddColumn("dbo.Employee", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ImageName");
            DropColumn("dbo.Employee", "UpdatedDateBy");
        }
    }
}
