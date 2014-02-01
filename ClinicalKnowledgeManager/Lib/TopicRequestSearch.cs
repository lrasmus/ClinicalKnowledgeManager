using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using CodeFirstStoredProcedures;
using HL7InfobuttonAPI;

namespace ClinicalKnowledgeManager.Lib
{
    public class TopicRequestSearch
    {
        private readonly ModelContext Context = null;
        public IContextQuery StoredProcedure { get; set; }

        public TopicRequestSearch(ModelContext context)
        {
            Context = context;
        }

        public List<Models.Topic> SearchTopics(IEnumerable<KeyValuePair<string, string>> queryString)
        {
            NameValueCollection collection = new NameValueCollection();
            foreach (var item in queryString)
            {
                collection.Add(item.Key, item.Value);
            }

            return SearchTopics(collection);
        }

        public List<Models.Topic> SearchTopics(NameValueCollection queryString)
        {
            var mapper = BuildMapperFromQueryString(queryString);
            StoredProcedure = new SearchForTopicsBasedOnContext
            {
                InformationRecipient = mapper.GetInformationRecipient(),
                SearchCode = mapper.GetSearchCode(),
                SearchCodeSystem = mapper.GetSearchCodeSystem(),
                Task = mapper.GetTaskCode(),
                SubTopicCode = mapper.GetSubTopicCode(),
                SubTopicCodeSystem = mapper.GetSubTopicCodeSystem(),
                Gender = mapper.GetGender(),
                AgeGroup = mapper.GetAge(),
                PerformerLanguage = mapper.GetPerformerLanguage(),
                RecipientLanguage = mapper.GetRecipientLanguage(),
                PerformerProviderCode = mapper.GetPerformerProviderCode(),
                RecipientProviderCode = mapper.GetRecipientProviderCode(),
                EncounterCode = mapper.GetEncounterCode()
            };
            var result = Context.Database.ExecuteStoredProcedure(StoredProcedure as SearchForTopicsBasedOnContext).ToList();
            return result;
        }

        public List<Models.SubTopic> SearchSubTopicsForTopic(int topicId, NameValueCollection queryString)
        {
            var mapper = BuildMapperFromQueryString(queryString);
            StoredProcedure = new GetSubTopicsForContext
                {
                    TopicID = topicId,
                    InformationRecipient = mapper.GetInformationRecipient(),
                    SearchCode = mapper.GetSearchCode(),
                    SearchCodeSystem = mapper.GetSearchCodeSystem(),
                    Task = mapper.GetTaskCode(),
                    SubTopicCode = mapper.GetSubTopicCode(),
                    SubTopicCodeSystem = mapper.GetSubTopicCodeSystem(),
                    Gender = mapper.GetGender(),
                    AgeGroup = mapper.GetAge(),
                    PerformerLanguage = mapper.GetPerformerLanguage(),
                    RecipientLanguage = mapper.GetRecipientLanguage(),
                    PerformerProviderCode = mapper.GetPerformerProviderCode(),
                    RecipientProviderCode = mapper.GetRecipientProviderCode(),
                    EncounterCode = mapper.GetEncounterCode()
                };
            var result = Context.Database.ExecuteStoredProcedure(StoredProcedure as GetSubTopicsForContext).ToList();
            return result;
        } 
        
        public QueryMapper BuildMapperFromQueryString(NameValueCollection queryString)
        {
            var parser = new Parser();
            var request = parser.ParseRequest(queryString);
            var mapper = new QueryMapper(request);
            return mapper;
        }
    }
}