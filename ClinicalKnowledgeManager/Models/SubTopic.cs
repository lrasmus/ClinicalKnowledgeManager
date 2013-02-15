using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicalKnowledgeManager.Models
{
    public class SubTopic : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string ParentType { get; set; }
    }
}