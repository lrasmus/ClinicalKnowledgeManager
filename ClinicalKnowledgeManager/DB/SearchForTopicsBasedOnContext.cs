using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstStoredProcedures;
using ClinicalKnowledgeManager.Models;

namespace ClinicalKnowledgeManager.DB
{
    public class SearchForTopicsBasedOnContext : IStoredProcedure<Topic>, IContextQuery
    {
        public SearchForTopicsBasedOnContext()
        {
            InformationRecipient = string.Empty;
            SearchCode = string.Empty;
            SearchCodeSystem = string.Empty;
            Task = string.Empty;
            SubTopicCode = string.Empty;
            SubTopicCodeSystem = string.Empty;
            Gender = string.Empty;
            AgeGroup = string.Empty;
        }

        public string InformationRecipient { get; set; }
        public string SearchCode { get; set; }
        public string SearchCodeSystem { get; set; }
        public string Task { get; set; }
        public string SubTopicCode { get; set; }
        public string SubTopicCodeSystem { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
    }
}