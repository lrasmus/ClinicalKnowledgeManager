namespace ClinicalKnowledgeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubTopics", "ParentType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubTopics", "ParentType", c => c.Int(nullable: false));
        }
    }
}
