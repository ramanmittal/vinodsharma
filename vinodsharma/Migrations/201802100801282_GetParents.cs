namespace vinodsharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class GetParents : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetParents", x => new { memberID = x.Int() }, @"with tbParent as
(
   select * from Members where [MemberID] = @memberID
   union all
   select Members.* from Members  join tbParent  on Members.[MemberID] = tbParent.UplineId


)
 SELECT * FROM  tbParent
  where tbParent.MemberID <> @memberID; ");
        }

        public override void Down()
        {
            DropStoredProcedure("GetParents");
        }
    }
}
