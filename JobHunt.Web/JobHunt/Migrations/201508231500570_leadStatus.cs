namespace JobHunt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leadStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leads", "Status", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leads", "Status", c => c.String());
        }
    }
}
