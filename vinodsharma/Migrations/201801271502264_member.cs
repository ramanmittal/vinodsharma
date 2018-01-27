namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class member : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        CoDistributerDOB = c.DateTime(nullable: false),
                        CoDistributerName = c.String(),
                        CoDistributerRelation = c.String(),
                        Dob = c.DateTime(nullable: false),
                        DistributerID = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Points = c.Int(nullable: false),
                        UplineId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.Members", t => t.UplineId)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberID)
                .Index(t => t.MemberID)
                .Index(t => t.UplineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "MemberID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Members", "UplineId", "dbo.Members");
            DropIndex("dbo.Members", new[] { "UplineId" });
            DropIndex("dbo.Members", new[] { "MemberID" });
            DropTable("dbo.Members");
        }
    }
}
