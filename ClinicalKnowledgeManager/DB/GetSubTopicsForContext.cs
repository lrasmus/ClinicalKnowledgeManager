using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstStoredProcedures;
using ClinicalKnowledgeManager.Models;

namespace ClinicalKnowledgeManager.DB
{
    public class GetSubTopicsForContext : IStoredProcedure<SubTopic>, IContextQuery
    {
        public int TopicID { get; set; }
        public string InformationRecipient { get; set; }
        public string SearchCode { get; set; }
        public string SearchCodeSystem { get; set; }
    }
}