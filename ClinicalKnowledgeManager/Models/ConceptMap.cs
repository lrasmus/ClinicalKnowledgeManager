using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicalKnowledgeManager.Models
{
    public class ConceptMap : BaseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeSet { get; set; }
        public int ParentId { get; set; }
        public string ParentType { get; set; }
    }
}
