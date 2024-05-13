namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "UserId");
        }
    }
}
