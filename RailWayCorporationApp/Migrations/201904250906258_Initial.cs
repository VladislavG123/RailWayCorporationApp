namespace RailWayCorporationApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carriages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Train_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trains", t => t.Train_Id)
                .Index(t => t.Train_Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsRent = c.Boolean(nullable: false),
                        Carriage_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carriages", t => t.Carriage_Id)
                .Index(t => t.Carriage_Id);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TrainNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DepartureCity = c.String(),
                        ArrivalCity = c.String(),
                        DepartureDate = c.DateTime(nullable: false),
                        ArrivalDate = c.DateTime(nullable: false),
                        Place_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.Place_Id)
                .Index(t => t.Place_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Place_Id", "dbo.Places");
            DropForeignKey("dbo.Carriages", "Train_Id", "dbo.Trains");
            DropForeignKey("dbo.Places", "Carriage_Id", "dbo.Carriages");
            DropIndex("dbo.Tickets", new[] { "Place_Id" });
            DropIndex("dbo.Places", new[] { "Carriage_Id" });
            DropIndex("dbo.Carriages", new[] { "Train_Id" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Trains");
            DropTable("dbo.Places");
            DropTable("dbo.Carriages");
        }
    }
}
