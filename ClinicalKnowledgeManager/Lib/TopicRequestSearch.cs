using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using HL7InfobuttonAPI;

namespace ClinicalKnowledgeManager.Lib
{
    public class TopicRequestSearch
    {
        public TopicRepository Repository { get; set; }

        public TopicRequestSearch(TopicRepository repository)
        {
            TopicRepository = topicRepo;
            SubTopicRepository = subTopicRepo;
        }

        public List<Topic> SearchTopics(IEnumerable<KeyValuePair<string, string>> queryString)
        {
            var collection = new NameValueCollection();
            foreach (var item in queryString)
            {
                collection.Add(item.Key, item.Value);
            }

            return SearchTopics(collection);
        }

        public List<Topic> SearchTopics(NameValueCollection queryString)
        {
            var mapper = BuildMapperFromQueryString(queryString);
            return TopicRepository.SearchForTopicsBasedOnContext(mapper.GetInformationRecipient(), mapper.GetSearchCode(), mapper.GetSearchCodeSystem(), mapper.GetSearchTerm(),
                mapper.GetTaskCode(), mapper.GetSubTopicCode(), mapper.GetSubTopicCodeSystem(), mapper.GetSubTopicTerm(), mapper.GetGender(), mapper.GetAge(),
                mapper.GetPerformerLanguage(), mapper.GetRecipientLanguage(), mapper.GetPerformerProviderCode(), mapper.GetRecipientProviderCode(), mapper.GetEncounterCode());
        }

        public List<SubTopic> SearchSubTopicsForTopic(int topicId, IEnumerable<KeyValuePair<string, string>> queryString)
        {
            NameValueCollection collection = new NameValueCollection();
            foreach (var item in queryString)
            {
                collection.Add(item.Key, item.Value);
            }

            return SearchSubTopicsForTopic(topicId, collection);
        }

        public List<SubTopic> SearchSubTopicsForTopic(int topicId, NameValueCollection queryString)
        {
            var mapper = BuildMapperFromQueryString(queryString);
            return SubTopicRepository.GetSubTopicsForContext(topicId, mapper.GetInformationRecipient(), mapper.GetSearchCode(), mapper.GetSearchCodeSystem(), mapper.GetSearchTerm(),
                mapper.GetTaskCode(), mapper.GetSubTopicCode(), mapper.GetSubTopicCodeSystem(), mapper.GetSubTopicTerm(), mapper.GetGender(), mapper.GetAge(),
                mapper.GetPerformerLanguage(), mapper.GetRecipientLanguage(), mapper.GetPerformerProviderCode(), mapper.GetRecipientProviderCode(), mapper.GetEncounterCode());
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