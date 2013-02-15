using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicalKnowledgeManager.Models
{
    public class Content : BaseModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ParentId { get; set; }
        public string ParentType { get; set; }
    }
}
