namespace ClinicalKnowledgeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCodeSetToCodeSystem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConceptMaps", "CodeSystem", c => c.String(maxLength: 255));
            DropColumn("dbo.ConceptMaps", "CodeSet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConceptMaps", "CodeSet", c => c.String(maxLength: 255));
            DropColumn("dbo.ConceptMaps", "CodeSystem");
        }
    }
}
