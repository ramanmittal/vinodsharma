namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amountgiven : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemeberAmountHistories",
                c => new
                    {
                        MemeberAmountHistoryID = c.Int(nullable: false, identity: true),
                        MemeberID = c.Int(nullable: false),
                        AmountGiven = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.MemeberAmountHistoryID)
                .ForeignKey("dbo.Members", t => t.MemeberID, cascadeDelete: true)
                .Index(t => t.MemeberID);
            
            AddColumn("dbo.Members", "AmountGiven", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemeberAmountHistories", "MemeberID", "dbo.Members");
            DropIndex("dbo.MemeberAmountHistories", new[] { "MemeberID" });
            DropColumn("dbo.Members", "AmountGiven");
            DropTable("dbo.MemeberAmountHistories");
        }
    }
}
