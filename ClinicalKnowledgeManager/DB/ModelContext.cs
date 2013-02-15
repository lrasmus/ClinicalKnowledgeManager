using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ClinicalKnowledgeManager.Models;

namespace ClinicalKnowledgeManager.DB
{
    public class ModelContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<SubTopic> SubTopics { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ConceptMap> ConceptMaps { get; set; }
    }
}