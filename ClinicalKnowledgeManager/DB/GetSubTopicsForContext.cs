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
        public GetSubTopicsForContext()
        {
            InformationRecipient = string.Empty;
            SearchCode = string.Empty;
            SearchCodeSystem = string.Empty;
            SearchTerm = string.Empty;
            Task = string.Empty;
            SubTopicCode = string.Empty;
            SubTopicCodeSystem = string.Empty;
            SubTopicTerm = string.Empty;
            Gender = string.Empty;
            AgeGroup = string.Empty;
            PerformerLanguage = string.Empty;
            RecipientLanguage = string.Empty;
            PerformerProviderCode = string.Empty;
            RecipientProviderCode = string.Empty;
            EncounterCode = string.Empty;
        }

        public int TopicID { get; set; }
        public string InformationRecipient { get; set; }
        public string SearchCode { get; set; }
        public string SearchCodeSystem { get; set; }
        public string SearchTerm { get; set; }
        public string Task { get; set; }
        public string SubTopicCode { get; set; }
        public string SubTopicCodeSystem { get; set; }
        public string SubTopicTerm { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public string PerformerLanguage { get; set; }
        public string RecipientLanguage { get; set; }
        public string PerformerProviderCode { get; set; }
        public string RecipientProviderCode { get; set; }
        public string EncounterCode { get; set; }
    }
}