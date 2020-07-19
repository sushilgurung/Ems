namespace Library.DataAcessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class createtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLog",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TableName = c.String(maxLength: 100),
                    TableId = c.Int(nullable: false),
                    Description = c.String(maxLength: 300),
                    UserId = c.Guid(nullable: false),
                    IpAddress = c.String(),
                    AddedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Employee",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FullName = c.String(maxLength: 400),
                    DateOfBirth = c.DateTime(nullable: false),
                    Gender = c.Int(nullable: false),
                    Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Designation = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    DeletedOn = c.DateTime(),
                    IsUpdated = c.Boolean(nullable: false, defaultValue: false),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Gender",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 100),
                    CreatedOn = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    DeletedOn = c.DateTime(),
                    IsUpdated = c.Boolean(nullable: false, defaultValue: false),
                    UpdatedDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Log",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Message = c.String(),
                    ExceptionMessage = c.String(),
                    IpAddressPrivate = c.String(),
                    IpAddressPublic = c.String(),
                    PageName = c.String(),
                    Createdby = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    IsActive = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.LoginActivities",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Guid(nullable: false),
                    LoginTime = c.DateTime(nullable: false),
                    IpAddress = c.String(maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RoleId = c.Guid(nullable: false),
                    RoleName = c.String(),
                    RoleFriendlyName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SuspendedIP",
                c => new
                {
                    IPAddressID = c.Int(nullable: false, identity: true),
                    IpAddress = c.String(nullable: false, maxLength: 30),
                    IpAddressPublic = c.String(),
                    SuspendedTime = c.DateTime(nullable: false),
                    IsSuspended = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.IPAddressID);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Guid(nullable: false),
                    FirstName = c.String(),
                    MiddleName = c.String(),
                    LastName = c.String(),
                    UserName = c.String(nullable: false, maxLength: 100),
                    Password = c.String(),
                    PasswordFormat = c.Int(nullable: false),
                    PasswordSalt = c.String(),
                    Email = c.String(),
                    IsActive = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    DeletedBy = c.Guid(),
                    CreatedOn = c.DateTime(),
                    CreatedBy = c.Guid(nullable: false),
                    IsModified = c.Boolean(nullable: false),
                    UpdatedOn = c.DateTime(),
                    LastLoginDate = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserInRoles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RoleId = c.Guid(nullable: false),
                    UserId = c.Guid(nullable: false),
                    CreateBy = c.Guid(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.UserInRoles");
            DropTable("dbo.User");
            DropTable("dbo.SuspendedIP");
            DropTable("dbo.Roles");
            DropTable("dbo.LoginActivities");
            DropTable("dbo.Log");
            DropTable("dbo.Gender");
            DropTable("dbo.Employee");
            DropTable("dbo.ActivityLog");
        }
    }
}
