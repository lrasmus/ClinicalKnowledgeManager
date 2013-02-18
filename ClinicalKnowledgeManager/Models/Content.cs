using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ClinicalKnowledgeManager.Models
{
    public class Content : BaseModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ParentId { get; set; }
        [MaxLength(50)]
        public string ParentType { get; set; }
    }
}
