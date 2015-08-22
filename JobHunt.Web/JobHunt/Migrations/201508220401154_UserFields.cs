namespace JobHunt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Resume", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "CoverLetterBoilerplate", c => c.Binary());
            AddColumn("dbo.AspNetUsers", "ProfilePic", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePic");
            DropColumn("dbo.AspNetUsers", "CoverLetterBoilerplate");
            DropColumn("dbo.AspNetUsers", "Resume");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
