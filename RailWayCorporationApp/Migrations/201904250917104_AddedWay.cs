namespace RailWayCorporationApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ways",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartureCity = c.String(),
                        ArrivalCity = c.String(),
                        DepartureDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        Train_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trains", t => t.Train_Id)
                .Index(t => t.Train_Id);
            
            AddColumn("dbo.Tickets", "Way_Id", c => c.Guid());
            CreateIndex("dbo.Tickets", "Way_Id");
            AddForeignKey("dbo.Tickets", "Way_Id", "dbo.Ways", "Id");
            DropColumn("dbo.Tickets", "DepartureCity");
            DropColumn("dbo.Tickets", "ArrivalCity");
            DropColumn("dbo.Tickets", "DepartureDate");
            DropColumn("dbo.Tickets", "ArrivalDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "ArrivalDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tickets", "DepartureDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tickets", "ArrivalCity", c => c.String());
            AddColumn("dbo.Tickets", "DepartureCity", c => c.String());
            DropForeignKey("dbo.Ways", "Train_Id", "dbo.Trains");
            DropForeignKey("dbo.Tickets", "Way_Id", "dbo.Ways");
            DropIndex("dbo.Ways", new[] { "Train_Id" });
            DropIndex("dbo.Tickets", new[] { "Way_Id" });
            DropColumn("dbo.Tickets", "Way_Id");
            DropTable("dbo.Ways");
        }
    }
}
