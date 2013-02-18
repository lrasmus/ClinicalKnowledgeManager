namespace ClinicalKnowledgeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetFeldLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Topics", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Topics", "Audience", c => c.String(maxLength: 50));
            AlterColumn("dbo.SubTopics", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.SubTopics", "ParentType", c => c.String(maxLength: 50));
            AlterColumn("dbo.Contents", "ParentType", c => c.String(maxLength: 50));
            AlterColumn("dbo.ConceptMaps", "Code", c => c.String(maxLength: 255));
            AlterColumn("dbo.ConceptMaps", "CodeSet", c => c.String(maxLength: 255));
            AlterColumn("dbo.ConceptMaps", "ParentType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ConceptMaps", "ParentType", c => c.String());
            AlterColumn("dbo.ConceptMaps", "CodeSet", c => c.String());
            AlterColumn("dbo.ConceptMaps", "Code", c => c.String());
            AlterColumn("dbo.Contents", "ParentType", c => c.String());
            AlterColumn("dbo.SubTopics", "ParentType", c => c.String());
            AlterColumn("dbo.SubTopics", "Name", c => c.String());
            AlterColumn("dbo.Topics", "Audience", c => c.String());
            AlterColumn("dbo.Topics", "Name", c => c.String());
        }
    }
}
