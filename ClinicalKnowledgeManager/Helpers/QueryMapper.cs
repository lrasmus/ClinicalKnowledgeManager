using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HL7InfobuttonAPI.Models;

namespace ClinicalKnowledgeManager.Helpers
{
    public class QueryMapper
    {
        protected KnowledgeRequestNotification Query { get; set; }

        public QueryMapper(KnowledgeRequestNotification query)
        {
            Query = query;
        }

        public string GetInformationRecipient()
        {
            if (Query.InformationRecipient == null
                || string.IsNullOrWhiteSpace(Query.InformationRecipient.Value))
            {
                return string.Empty;
            }

            return Query.InformationRecipient.Value;
        }

        public string GetSearchCode()
        {
            if (Query.MainSearchCriteria == null
                || Query.MainSearchCriteria.Value == null
                || string.IsNullOrWhiteSpace(Query.MainSearchCriteria.Value.Code))
            {
                return string.Empty;
            }

            return Query.MainSearchCriteria.Value.Code;
        }

        public string GetSearchCodeSystem()
        {
            if (Query.MainSearchCriteria == null
                || Query.MainSearchCriteria.Value == null
                || string.IsNullOrWhiteSpace(Query.MainSearchCriteria.Value.CodeSystem))
            {
                return string.Empty;
            }

            return Query.MainSearchCriteria.Value.CodeSystem;
        }
    }
}