using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProjectTAble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        Manager_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.Manager_ID)
                .Index(t => t.Manager_ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .Index(t => t.Project_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Members", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Manager_ID", "dbo.Members");
            DropIndex("dbo.Tasks", new[] { "Project_ID" });
            DropIndex("dbo.Projects", new[] { "Manager_ID" });
            DropIndex("dbo.Members", new[] { "Project_ID" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Projects");
            DropTable("dbo.Members");
        }
    }
}
