namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maxvalue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "MaxValue", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "MaxValue");
        }
    }
}
