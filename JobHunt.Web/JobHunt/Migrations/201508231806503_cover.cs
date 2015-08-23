namespace JobHunt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cover : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoverLetters",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leads", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Leads", "CoverLetter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leads", "CoverLetter", c => c.String());
            DropForeignKey("dbo.CoverLetters", "Id", "dbo.Leads");
            DropIndex("dbo.CoverLetters", new[] { "Id" });
            DropTable("dbo.CoverLetters");
        }
    }
}
