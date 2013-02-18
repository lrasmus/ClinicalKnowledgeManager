using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClinicalKnowledgeManager.Models
{
    public class SubTopic : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public int ParentId { get; set; }
        [MaxLength(50)]
        public string ParentType { get; set; }
    }
}