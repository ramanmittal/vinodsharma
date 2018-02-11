namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "HasDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "HasDeleted");
        }
    }
}
