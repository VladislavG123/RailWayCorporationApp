namespace RailWayCorporationApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tickets", "User_Id", c => c.Guid());
            CreateIndex("dbo.Tickets", "User_Id");
            AddForeignKey("dbo.Tickets", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "User_Id", "dbo.Users");
            DropIndex("dbo.Tickets", new[] { "User_Id" });
            DropColumn("dbo.Tickets", "User_Id");
            DropTable("dbo.Users");
        }
    }
}
