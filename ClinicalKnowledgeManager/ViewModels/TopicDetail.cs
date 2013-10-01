using System.Collections.Generic;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Models;

namespace ClinicalKnowledgeManager.ViewModels
{
    public class TopicDetail
    {
        public Topic Topic { get; set; }
        public IEnumerable<SubTopicDetail> SubTopics { get; set; }
        public IEnumerable<SubTopic> ContextSubTopics { get; set; }
        public IContextQuery ContextQuery { get; set; }
    }
}