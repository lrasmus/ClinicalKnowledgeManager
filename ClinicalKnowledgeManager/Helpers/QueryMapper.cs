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

        public string GetSearchTerm()
        {
            if (Query.MainSearchCriteria == null
                || Query.MainSearchCriteria.Value == null
                || string.IsNullOrWhiteSpace(Query.MainSearchCriteria.Value.DisplayName))
            {
                return string.Empty;
            }

            return Query.MainSearchCriteria.Value.DisplayName;
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

        public string GetSubTopicTerm()
        {
            if (Query.SubTopic == null
                || Query.SubTopic.Value == null
                || string.IsNullOrWhiteSpace(Query.SubTopic.Value.DisplayName))
            {
                return string.Empty;
            }

            return Query.SubTopic.Value.DisplayName;
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

        public string GetPerformerLanguage()
        {
            if (Query.Performer == null
                || Query.Performer.LanguageCode == null
                || string.IsNullOrWhiteSpace(Query.Performer.LanguageCode.Code))
            {
                return string.Empty;
            }

            return Query.Performer.LanguageCode.Code;
        }

        public string GetRecipientLanguage()
        {
            if (Query.InformationRecipient == null
                || Query.InformationRecipient.LanguageCode == null
                || string.IsNullOrWhiteSpace(Query.InformationRecipient.LanguageCode.Code))
            {
                return string.Empty;
            }

            return Query.InformationRecipient.LanguageCode.Code;
        }

        public string GetPerformerProviderCode()
        {
            if (Query.Performer == null
                || Query.Performer.HealthCareProvider == null
                || Query.Performer.HealthCareProvider.Code == null
                || string.IsNullOrWhiteSpace(Query.Performer.HealthCareProvider.Code.Code))
            {
                return string.Empty;
            }

            return Query.Performer.HealthCareProvider.Code.Code;
        }

        public string GetRecipientProviderCode()
        {
            if (Query.InformationRecipient == null
                || Query.InformationRecipient.HealthCareProvider == null
                || Query.InformationRecipient.HealthCareProvider.Code == null
                || string.IsNullOrWhiteSpace(Query.InformationRecipient.HealthCareProvider.Code.Code))
            {
                return string.Empty;
            }

            return Query.InformationRecipient.HealthCareProvider.Code.Code;
        }

        public string GetEncounterCode()
        {
            if (Query.Encounter == null
                || Query.Encounter.Code == null
                || string.IsNullOrWhiteSpace(Query.Encounter.Code.Code))
            {
                return string.Empty;
            }

            return Query.Encounter.Code.Code;
        }
    }
}