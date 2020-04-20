namespace SimplePayment.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MerchantInfo",
                c => new
                    {
                        MerchantId = c.Int(nullable: false, identity: true),
                        MerchantName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.MerchantId);
            
            CreateTable(
                "dbo.TranscationInfo",
                c => new
                    {
                        TranscationId = c.Int(nullable: false, identity: true),
                        MerchantId = c.Int(nullable: false),
                        MerchantName = c.String(),
                        ReferenceFlag = c.String(nullable: false, maxLength: 50),
                        PayAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payway = c.Byte(nullable: false),
                        PayTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TranscationId)
                .ForeignKey("dbo.MerchantInfo", t => t.MerchantId, cascadeDelete: true)
                .Index(t => t.MerchantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TranscationInfo", "MerchantId", "dbo.MerchantInfo");
            DropIndex("dbo.TranscationInfo", new[] { "MerchantId" });
            DropTable("dbo.TranscationInfo");
            DropTable("dbo.MerchantInfo");
        }
    }
}
