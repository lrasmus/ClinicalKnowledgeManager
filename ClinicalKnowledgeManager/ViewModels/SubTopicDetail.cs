using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.ViewModels
{
    public class SubTopicDetail
    {
        public int Level { get; set; }
        public SubTopic SubTopic { get; set; }
        public IEnumerable<string> Contents { get; set; }
        public IEnumerable<SubTopicDetail> SubTopics { get; set; }
        public bool IsOnlyItemDisplayed { get; set; }
        public bool IsContextItem { get; set; }
    }
}
