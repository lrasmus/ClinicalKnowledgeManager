using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Filters;
using ClinicalKnowledgeManager.Helpers;
using ClinicalKnowledgeManager.ViewModels;

namespace ClinicalKnowledgeManager.Controllers
{
    [ApiLogFilter]
    public class TopicsApiController : ApiController
    {
        private readonly TopicRepository Repository = null;
        private readonly ViewModelFactory Factory;

        public TopicsApiController()
        {
            Repository = new TopicRepository();
            Factory = new ViewModelFactory(Repository);
        }

        public TopicsApiController(string contextName)
        {
            Repository = new TopicRepository(contextName);
            Factory = new ViewModelFactory(Repository);
        }

        [HttpGet]
        [ActionName("FindFirstMatchingTopic")]
        public HttpResponseMessage FindFirstMatchingTopic()
        {
            var result = Repository.SearchTopics(Request.GetQueryNameValuePairs());

            if (result.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var topic = result.FirstOrDefault();
            var relevantSubTopics = Repository.SearchSubTopicsForTopic(topic.Id, Request.GetQueryNameValuePairs());
            return Request.CreateResponse(HttpStatusCode.OK, 
                new TopicSearchResult
                {
                    Topics = new List<TopicDetail>() { Factory.BuildTopicDetails(topic, relevantSubTopics) }
                });
        }
    }
}
