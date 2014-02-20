using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.ViewModels
{
    public class TopicSearchResult
    {
        public IEnumerable<TopicDetail> Topics { get; set; }
        //public IContextQuery ContextQuery { get; set; }
    }
}