namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "UserId");
        }
    }
}
