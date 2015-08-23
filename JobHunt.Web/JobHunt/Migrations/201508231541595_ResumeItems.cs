namespace JobHunt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResumeItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leads", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ResumeItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        Resume_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.Resume_Id)
                .Index(t => t.Resume_Id);
            
            DropColumn("dbo.Leads", "Resume");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leads", "Resume", c => c.String());
            DropForeignKey("dbo.Resumes", "Id", "dbo.Leads");
            DropForeignKey("dbo.ResumeItems", "Resume_Id", "dbo.Resumes");
            DropIndex("dbo.ResumeItems", new[] { "Resume_Id" });
            DropIndex("dbo.Resumes", new[] { "Id" });
            DropTable("dbo.ResumeItems");
            DropTable("dbo.Resumes");
        }
    }
}
