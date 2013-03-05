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

        public string GetTaskCode()
        {
            if (Query.TaskContext == null
                || Query.TaskContext.Code == null
                || string.IsNullOrWhiteSpace(Query.TaskContext.Code.Code))
            {
                return string.Empty;
            }

            return Query.TaskContext.Code.Code;
        }

        public string GetSubTopicCode()
        {
            if (Query.SubTopic == null
                || Query.SubTopic.Value == null
                || string.IsNullOrWhiteSpace(Query.SubTopic.Value.Code))
            {
                return string.Empty;
            }

            return Query.SubTopic.Value.Code;
        }

        public string GetSubTopicCodeSystem()
        {
            if (Query.SubTopic == null
                || Query.SubTopic.Value == null
                || string.IsNullOrWhiteSpace(Query.SubTopic.Value.CodeSystem))
            {
                return string.Empty;
            }

            return Query.SubTopic.Value.CodeSystem;
        }

        public string GetGender()
        {
            if (Query.PatientContext == null
                || Query.PatientContext.PatientPerson == null
                || Query.PatientContext.PatientPerson.AdministrativeGenderCode == null
                || string.IsNullOrWhiteSpace(Query.PatientContext.PatientPerson.AdministrativeGenderCode.Code))
            {
                return string.Empty;
            }

            return Query.PatientContext.PatientPerson.AdministrativeGenderCode.Code;
        }

        public string GetAge()
        {
            if (Query.PatientContext == null
                || Query.PatientContext.AgeGroup == null
                || Query.PatientContext.AgeGroup.Value == null
                || string.IsNullOrWhiteSpace(Query.PatientContext.AgeGroup.Value.Code))
            {
                return string.Empty;
            }

            return Query.PatientContext.AgeGroup.Value.Code;
        }
    }
}