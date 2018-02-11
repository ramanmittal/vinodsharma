namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profilechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "CoDistributerLastName", c => c.String());
            AddColumn("dbo.Members", "City", c => c.String());
            AddColumn("dbo.Members", "State", c => c.String());
            AddColumn("dbo.Members", "PinCode", c => c.String());
            RenameColumn("dbo.Members", "CoDistributerName", "CoDistributerFirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "CoDistributerName", c => c.String());
            DropColumn("dbo.Members", "PinCode");
            DropColumn("dbo.Members", "State");
            DropColumn("dbo.Members", "City");
            DropColumn("dbo.Members", "CoDistributerLastName");
            DropColumn("dbo.Members", "CoDistributerFirstName");
        }
    }
}
