using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicalKnowledgeManager.Models
{
    public class Topic : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Audience { get; set; }
        public string InternalComments { get; set; }
    }
}