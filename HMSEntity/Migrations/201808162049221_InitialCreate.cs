namespace HMSEntity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        WorkingSince = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Salary = c.Double(nullable: false),
                        Address = c.String(nullable: false),
                        MobileNo = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        UserId = c.String(),
                        UserType = c.String(nullable: false),
                        NID = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        Picture = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(nullable: false),
                        RoomType = c.String(nullable: false),
                        NoOfBed = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Picture = c.Binary(),
                        Status = c.String(),
                        BookedDate = c.DateTime(nullable: false),
                        CheckedInDate = c.DateTime(nullable: false),
                        CheckedOutDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
            DropTable("dbo.Employees");
        }
    }
}
