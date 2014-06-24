using System.Collections.Generic;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.ViewModels
{
    public class TopicDetail
    {
        public Topic Topic { get; set; }
        public bool ShowTableOfContents { get; set; }
        public IEnumerable<SubTopicDetail> SubTopics { get; set; }
        public IEnumerable<SubTopic> ContextSubTopics { get; set; }
    }
}