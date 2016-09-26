namespace GasMonitor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "gas.FillUps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VehicleId = c.Guid(nullable: false),
                        Gallons = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Miles = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrimarilyHighway = c.Boolean(nullable: false),
                        FilledAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gas.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);

            CreateTable(
                "gas.Vehicles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(),
                        VehicleType = c.Byte(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("gas.Owners", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);

            CreateTable(
                "gas.Owners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("gas.FillUps", "VehicleId", "gas.Vehicles");
            DropForeignKey("gas.Vehicles", "OwnerId", "gas.Owners");
            DropIndex("gas.Vehicles", new[] { "OwnerId" });
            DropIndex("gas.FillUps", new[] { "VehicleId" });
            DropTable("gas.Owners");
            DropTable("gas.Vehicles");
            DropTable("gas.FillUps");
        }
    }
}
