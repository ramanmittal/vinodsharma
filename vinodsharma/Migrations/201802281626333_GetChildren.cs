namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetChildren : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetChildren", x => new { memberID = x.Int() }, @" with tbParent as  
            (  
            select * from Members where [MemberID] = @memberID  
            union all  
            select Members.* from Members  join tbParent  on Members.[UplineId] = tbParent.MemberID  
            )  
            SELECT * FROM  tbParent  
            where tbParent.MemberID <> @memberID;   ");
        }

        public override void Down()
        {
            DropStoredProcedure("GetChildren");
        }
    }
}
