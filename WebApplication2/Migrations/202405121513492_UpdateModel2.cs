namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Project_ID", "dbo.Projects");
            AddForeignKey("dbo.Tasks", "Project_ID", "dbo.Projects", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_ID", "dbo.Projects");
            AddForeignKey("dbo.Tasks", "Project_ID", "dbo.Projects", "ID");
        }
    }
}
