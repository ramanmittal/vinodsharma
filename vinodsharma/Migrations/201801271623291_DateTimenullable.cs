namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "CoDistributerDOB", c => c.DateTime());
            AlterColumn("dbo.Members", "Dob", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Dob", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Members", "CoDistributerDOB", c => c.DateTime(nullable: false));
        }
    }
}
