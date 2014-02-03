using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ClinicalKnowledgeManager.Models
{
    public class ConceptMap : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string CodeSystem { get; set; }
        public int ParentId { get; set; }
        [MaxLength(50)]
        public string ParentType { get; set; }
        [MaxLength(255)]
        public string Term { get; set; }
    }
}
