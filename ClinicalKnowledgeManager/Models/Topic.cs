using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicalKnowledgeManager.Models
{
    public class Topic : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Audience { get; set; }
        public string InternalComments { get; set; }
    }
}