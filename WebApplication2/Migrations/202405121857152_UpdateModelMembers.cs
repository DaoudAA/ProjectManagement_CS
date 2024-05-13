namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelMembers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "Project_ID", "dbo.Projects");
            DropIndex("dbo.Members", new[] { "Project_ID" });
            CreateTable(
                "dbo.ProjectMembers",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.MemberId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.MemberId);
            
            AddColumn("dbo.Members", "UserID", c => c.String());
            DropColumn("dbo.Members", "Project_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Project_ID", c => c.Int());
            DropForeignKey("dbo.ProjectMembers", "MemberId", "dbo.Members");
            DropForeignKey("dbo.ProjectMembers", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectMembers", new[] { "MemberId" });
            DropIndex("dbo.ProjectMembers", new[] { "ProjectId" });
            DropColumn("dbo.Members", "UserID");
            DropTable("dbo.ProjectMembers");
            CreateIndex("dbo.Members", "Project_ID");
            AddForeignKey("dbo.Members", "Project_ID", "dbo.Projects", "ID");
        }
    }
}
