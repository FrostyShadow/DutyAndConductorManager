namespace ISSK_2_0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrigadeConductors",
                c => new
                    {
                        BrigadeId = c.Int(nullable: false),
                        ConductorId = c.Int(nullable: false),
                        IsManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.BrigadeId, t.ConductorId })
                .ForeignKey("dbo.Brigades", t => t.BrigadeId, cascadeDelete: true)
                .ForeignKey("dbo.Conductors", t => t.ConductorId, cascadeDelete: true)
                .Index(t => t.BrigadeId)
                .Index(t => t.ConductorId);
            
            CreateTable(
                "dbo.Brigades",
                c => new
                    {
                        BrigadeId = c.Int(nullable: false, identity: true),
                        BrigadeNo = c.Int(nullable: false),
                        LineId = c.Int(nullable: false),
                        VehicleSetId = c.Int(nullable: false),
                        ServiceDateTime = c.DateTime(nullable: false),
                        VehicleSet_SetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrigadeId)
                .ForeignKey("dbo.Lines", t => t.LineId, cascadeDelete: true)
                .ForeignKey("dbo.Sets", t => t.VehicleSet_SetId, cascadeDelete: true)
                .Index(t => t.LineId)
                .Index(t => t.VehicleSet_SetId);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        LineId = c.Int(nullable: false, identity: true),
                        LineNumber = c.String(),
                        TypeId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        LineType_LineTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LineId)
                .ForeignKey("dbo.LineTypes", t => t.LineType_LineTypeId, cascadeDelete: true)
                .Index(t => t.LineType_LineTypeId);
            
            CreateTable(
                "dbo.LineTypes",
                c => new
                    {
                        LineTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LineTypeId);
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        SetId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SetId);
            
            CreateTable(
                "dbo.SetVehicles",
                c => new
                    {
                        SetId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SetId, t.VehicleId })
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .ForeignKey("dbo.Sets", t => t.SetId, cascadeDelete: true)
                .Index(t => t.SetId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        SideNo = c.String(),
                        IsCoupleable = c.Boolean(nullable: false),
                        TypeId = c.Int(nullable: false),
                        VehicleType_VehicleTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleType_VehicleTypeId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.Conductors",
                c => new
                    {
                        ConductorId = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        LastActiveDateTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.String(),
                    })
                .PrimaryKey(t => t.ConductorId);
            
            CreateTable(
                "dbo.ConductorDatas",
                c => new
                    {
                        ConductorDataId = c.Int(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Pesel = c.String(),
                        IsTrained = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        Avatar = c.String(),
                        ConductorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConductorDataId)
                .ForeignKey("dbo.Conductors", t => t.ConductorDataId, cascadeDelete: true)
                .Index(t => t.ConductorDataId);
            
            CreateTable(
                "dbo.MessageRecipients",
                c => new
                    {
                        RecipientId = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        RecipientConductor_ConductorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipientId, t.MessageId })
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("dbo.Conductors", t => t.RecipientConductor_ConductorId, cascadeDelete: true)
                .Index(t => t.MessageId)
                .Index(t => t.RecipientConductor_ConductorId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        SenderId = c.Int(nullable: false),
                        MessageBody = c.String(),
                        CreationDateTime = c.DateTime(nullable: false),
                        ParentMessageId = c.Int(),
                        SenderConductor_ConductorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Messages", t => t.ParentMessageId, cascadeDelete: false)
                .ForeignKey("dbo.Conductors", t => t.SenderConductor_ConductorId, cascadeDelete: false)
                .Index(t => t.ParentMessageId)
                .Index(t => t.SenderConductor_ConductorId);
            
            CreateTable(
                "dbo.NotificationRecipients",
                c => new
                    {
                        NotificationId = c.Int(nullable: false),
                        ConductorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NotificationId, t.ConductorId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.Conductors", t => t.ConductorId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.ConductorId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                        NotificationBody = c.String(),
                        CreatorConductor_ConductorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.Conductors", t => t.CreatorConductor_ConductorId, cascadeDelete: false)
                .Index(t => t.CreatorConductor_ConductorId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConductorRoles",
                c => new
                    {
                        ConductorId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConductorId, t.RoleId })
                .ForeignKey("dbo.Conductors", t => t.ConductorId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.ConductorId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConductorRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ConductorRoles", "ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.NotificationRecipients", "ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.NotificationRecipients", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "CreatorConductor_ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.Messages", "SenderConductor_ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.MessageRecipients", "RecipientConductor_ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.Messages", "ParentMessageId", "dbo.Messages");
            DropForeignKey("dbo.MessageRecipients", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.ConductorDatas", "ConductorDataId", "dbo.Conductors");
            DropForeignKey("dbo.BrigadeConductors", "ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.Brigades", "VehicleSet_SetId", "dbo.Sets");
            DropForeignKey("dbo.SetVehicles", "SetId", "dbo.Sets");
            DropForeignKey("dbo.Vehicles", "VehicleType_VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.SetVehicles", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Brigades", "LineId", "dbo.Lines");
            DropForeignKey("dbo.Lines", "LineType_LineTypeId", "dbo.LineTypes");
            DropForeignKey("dbo.BrigadeConductors", "BrigadeId", "dbo.Brigades");
            DropIndex("dbo.ConductorRoles", new[] { "RoleId" });
            DropIndex("dbo.ConductorRoles", new[] { "ConductorId" });
            DropIndex("dbo.Notifications", new[] { "CreatorConductor_ConductorId" });
            DropIndex("dbo.NotificationRecipients", new[] { "ConductorId" });
            DropIndex("dbo.NotificationRecipients", new[] { "NotificationId" });
            DropIndex("dbo.Messages", new[] { "SenderConductor_ConductorId" });
            DropIndex("dbo.Messages", new[] { "ParentMessageId" });
            DropIndex("dbo.MessageRecipients", new[] { "RecipientConductor_ConductorId" });
            DropIndex("dbo.MessageRecipients", new[] { "MessageId" });
            DropIndex("dbo.ConductorDatas", new[] { "ConductorDataId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleType_VehicleTypeId" });
            DropIndex("dbo.SetVehicles", new[] { "VehicleId" });
            DropIndex("dbo.SetVehicles", new[] { "SetId" });
            DropIndex("dbo.Lines", new[] { "LineType_LineTypeId" });
            DropIndex("dbo.Brigades", new[] { "VehicleSet_SetId" });
            DropIndex("dbo.Brigades", new[] { "LineId" });
            DropIndex("dbo.BrigadeConductors", new[] { "ConductorId" });
            DropIndex("dbo.BrigadeConductors", new[] { "BrigadeId" });
            DropTable("dbo.ConductorRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Notifications");
            DropTable("dbo.NotificationRecipients");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageRecipients");
            DropTable("dbo.ConductorDatas");
            DropTable("dbo.Conductors");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.SetVehicles");
            DropTable("dbo.Sets");
            DropTable("dbo.LineTypes");
            DropTable("dbo.Lines");
            DropTable("dbo.Brigades");
            DropTable("dbo.BrigadeConductors");
        }
    }
}
