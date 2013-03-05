// -----------------------------------------------------------------------
// <copyright file="InitializerForTesting.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Data.Entity;
using ClinicalKnowledgeManager.Models;

namespace ClinicalKnowledgeManager.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClinicalKnowledgeManager.DB;
    using System.Reflection;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InitializerForTesting : DropCreateDatabaseIfModelChanges<ModelContext>
    {
        protected override void Seed(ModelContext context)
        {
            base.Seed(context);

            Assembly thisAssembly = Assembly.Load("ClinicalKnowledgeManager");
            string script = (new StreamReader(thisAssembly.GetManifestResourceStream("ClinicalKnowledgeManager.DB.Scripts.SearchForTopicsBasedOnContext.sql"))).ReadToEnd();
            context.Database.ExecuteSqlCommand(script);
            script = (new StreamReader(thisAssembly.GetManifestResourceStream("ClinicalKnowledgeManager.DB.Scripts.GetSubTopicsForContext.sql"))).ReadToEnd();
            context.Database.ExecuteSqlCommand(script);

            context.Topics.Add(new Topic() { Name = "Clopidogrel", Audience = "Physician", CreatedOn = DateTime.Now });
            context.Topics.Add(new Topic() { Name = "Warfarin", Audience = "Physician", CreatedOn = DateTime.Now });
            context.Topics.Add(new Topic() { Name = "Clopidogrel", Audience = "Patient", CreatedOn = DateTime.Now });
            context.Topics.Add(new Topic() { Name = "Warfarin", Audience = "Patient", CreatedOn = DateTime.Now });

            context.SubTopics.Add(new SubTopic() {Name = "Summary", ParentId = 1, ParentType = "Topic", CreatedOn = DateTime.Now});
            context.SubTopics.Add(new SubTopic() {Name = "Management", ParentId = 1, ParentType = "Topic", CreatedOn = DateTime.Now});
            context.SubTopics.Add(new SubTopic() {Name = "Poor Metabolizers", ParentId = 2, ParentType = "SubTopic", CreatedOn = DateTime.Now});
            context.SubTopics.Add(new SubTopic() {Name = "Intermediate Metabolizers", ParentId = 2, ParentType = "SubTopic", CreatedOn = DateTime.Now});
            context.SubTopics.Add(new SubTopic() {Name = "Normal Metabolizers", ParentId = 2, ParentType = "SubTopic", CreatedOn = DateTime.Now});
            context.SubTopics.Add(new SubTopic() {Name = "Femal Treatment", ParentId = 2, ParentType = "SubTopic", CreatedOn = DateTime.Now});
            context.SubTopics.Add(new SubTopic() {Name = "Middle Age", ParentId = 3, ParentType = "Topic", CreatedOn = DateTime.Now});

            context.ConceptMaps.Add(new ConceptMap() { Code = "MLREV", ParentId = 1, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.5.4" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "MLREV", ParentId = 2, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.5.4" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "PROV", ParentId = 1, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "informationRecipient" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "PROV", ParentId = 2, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "informationRecipient" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "PAT", ParentId = 3, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "informationRecipient" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "PAT", ParentId = 4, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "informationRecipient" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "424500005", ParentId = 3, ParentType = "SubTopic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.96" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "424500005", ParentId = 3, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.96" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "424500005", ParentId = 4, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.1" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "Q000175", ParentId = 3, ParentType = "SubTopic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.177" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "Q000628", ParentId = 2, ParentType = "SubTopic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.177" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "Q000175", ParentId = 2, ParentType = "Topic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.177" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "F", ParentId = 6, ParentType = "SubTopic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.5.1" });
            context.ConceptMaps.Add(new ConceptMap() { Code = "D008875", ParentId = 7, ParentType = "SubTopic", CreatedOn = DateTime.Now, CodeSystem = "2.16.840.1.113883.6.177" });

            context.SaveChanges();
        }
    }
}
