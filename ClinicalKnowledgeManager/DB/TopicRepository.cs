using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.Helpers;
using HL7InfobuttonAPI;

namespace ClinicalKnowledgeManager.DB
{
    public class TopicRepository : BaseRepository
    {
        public TopicRepository() {}

        public TopicRepository(string context) : base(context) { }

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
            return SearchForTopicsBasedOnContext(mapper.GetInformationRecipient(), mapper.GetSearchCode(), mapper.GetSearchCodeSystem(), mapper.GetSearchTerm(),
                mapper.GetTaskCode(), mapper.GetSubTopicCode(), mapper.GetSubTopicCodeSystem(), mapper.GetSubTopicTerm(), mapper.GetGender(), mapper.GetAge(),
                mapper.GetPerformerLanguage(), mapper.GetRecipientLanguage(), mapper.GetPerformerProviderCode(), mapper.GetRecipientProviderCode(), mapper.GetEncounterCode());
        }

        public List<Topic> SearchForTopicsBasedOnContext(ContextParams parameters)
        {
            return SearchForTopicsBasedOnContext(parameters.InformationRecipient, parameters.SearchCode,
                                                 parameters.SearchCodeSystem, parameters.SearchTerm, parameters.Task,
                                                 parameters.SubTopicCode, parameters.SubTopicCodeSystem,
                                                 parameters.SubTopicTerm, parameters.Gender, parameters.AgeGroup,
                                                 parameters.PerformerLanguage, parameters.RecipientLanguage,
                                                 parameters.PerformerProviderCode, parameters.RecipientProviderCode,
                                                 parameters.EncounterCode);
        }


        private List<Topic> SearchForTopicsBasedOnContext(string informationRecipient, string searchCode,
                                                  string searchCodeSystem, string searchTerm, string task,
                                                  string subTopicCode, string subTopicCodeSystem,
                                                  string subTopicTerm, string gender, string ageGroup,
                                                  string performerLanguage, string recipientLanguage,
                                                  string performerProviderCode, string recipientProviderCode,
                                                  string encounterCode)
        {
            return Context.SearchForTopicsBasedOnContext(informationRecipient, searchCode, searchCodeSystem, searchTerm, task,
                                                  subTopicCode, subTopicCodeSystem,
                                                  subTopicTerm, gender, ageGroup, performerLanguage, recipientLanguage,
                                                  performerProviderCode, recipientProviderCode, encounterCode).ToList();
        }

        public List<Topic> GetTopics()
        {
            return Context.Topics.ToList();
        }

        public Topic GetTopicById(int id)
        {
            return Context.Topics.FirstOrDefault(x => x.Id == id);
        }

        public List<SubTopic> SearchSubTopicsForTopic(int topicId, IEnumerable<KeyValuePair<string, string>> queryString)
        {
            var collection = new NameValueCollection();
            foreach (var item in queryString)
            {
                collection.Add(item.Key, item.Value);
            }

            return SearchSubTopicsForTopic(topicId, collection);
        }

        public List<SubTopic> SearchSubTopicsForTopic(int topicId, NameValueCollection queryString)
        {
            var mapper = BuildMapperFromQueryString(queryString);
            return GetSubTopicsForContext(topicId, mapper.GetInformationRecipient(), mapper.GetSearchCode(), mapper.GetSearchCodeSystem(), mapper.GetSearchTerm(),
                mapper.GetTaskCode(), mapper.GetSubTopicCode(), mapper.GetSubTopicCodeSystem(), mapper.GetSubTopicTerm(), mapper.GetGender(), mapper.GetAge(),
                mapper.GetPerformerLanguage(), mapper.GetRecipientLanguage(), mapper.GetPerformerProviderCode(), mapper.GetRecipientProviderCode(), mapper.GetEncounterCode());
        }

        private QueryMapper BuildMapperFromQueryString(NameValueCollection queryString)
        {
            var parser = new Parser();
            var request = parser.ParseRequest(queryString);
            var mapper = new QueryMapper(request);
            return mapper;
        }

        public List<SubTopic> GetSubTopicsForContext(int? topicId, ContextParams parameters)
        {
            return GetSubTopicsForContext(topicId, parameters.InformationRecipient, parameters.SearchCode,
                                          parameters.SearchCodeSystem, parameters.SearchTerm, parameters.Task,
                                          parameters.SubTopicCode, parameters.SubTopicCodeSystem,
                                          parameters.SubTopicTerm, parameters.Gender, parameters.AgeGroup,
                                          parameters.PerformerLanguage, parameters.RecipientLanguage,
                                          parameters.PerformerProviderCode, parameters.RecipientProviderCode,
                                          parameters.EncounterCode);
        }

        private List<SubTopic> GetSubTopicsForContext(int? topicId, string informationRecipient, string searchCode,
                                  string searchCodeSystem, string searchTerm, string task,
                                  string subTopicCode, string subTopicCodeSystem,
                                  string subTopicTerm, string gender, string ageGroup,
                                  string performerLanguage, string recipientLanguage,
                                  string performerProviderCode, string recipientProviderCode,
                                  string encounterCode)
        {
            return Context.GetSubTopicsForContext(topicId, informationRecipient, searchCode, searchCodeSystem, searchTerm, task,
                                                  subTopicCode, subTopicCodeSystem,
                                                  subTopicTerm, gender, ageGroup, performerLanguage, recipientLanguage,
                                                  performerProviderCode, recipientProviderCode, encounterCode).ToList();
        }

        public List<SubTopic> GetSubTopics()
        {
            return Context.SubTopics.ToList();
        }

        public SubTopic GetSubTopicById(int id)
        {
            return Context.SubTopics.FirstOrDefault(x => x.Id == id);
        }

        public List<SubTopic> GetSubTopicsForItem(string parentType, int parentId)
        {
            return Context.SubTopics.Where(x => x.ParentType == parentType && x.ParentId == parentId).ToList();
        }

        public List<string> GetContentsForItem(string itemType, int itemId)
        {
            return Context.Contents.Where(c => c.ParentType == itemType && c.ParentId == itemId).Select(c => c.Value).ToList();
        }

        public void LogRequest(string clientDetails, string message)
        {
            CKMLog log = new CKMLog()
                {
                    ClientDetails = clientDetails,
                    Message = message,
                    CreatedOn = DateTime.Now
                };
            Context.CKMLogs.AddObject(log);
            Context.SaveChanges();
        }
    }
}