using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using ClinicalKnowledgeManager.Lib;
using ClinicalKnowledgeManager.Models;
using ClinicalKnowledgeManager.ViewModels;

namespace ClinicalKnowledgeManager.Controllers
{
    public class TopicsApiController : ApiController
    {
        private readonly ModelContext Context = new ModelContext();
        private readonly ViewModelFactory Factory;

        public TopicsApiController()
        {
            Factory = new ViewModelFactory(Context);
        }

        public TopicsApiController(string contextName)
        {
            Context = new ModelContext(contextName);
            Factory = new ViewModelFactory(Context);
        }

        [HttpGet]
        [ActionName("FindFirstMatchingTopic")]
        public HttpResponseMessage FindFirstMatchingTopic()
        {
            var search = new TopicRequestSearch(Context);
            var result = search.SearchTopics(Request.GetQueryNameValuePairs());

            if (result.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, 
                new TopicSearchResult
                {
                    Topics = Factory.BuildTopicDetails(new List<Topic>() { result.First() })
                });
        }
    }
}
