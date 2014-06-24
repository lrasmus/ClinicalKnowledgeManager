using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Filters;
using ClinicalKnowledgeManager.Helpers;
using ClinicalKnowledgeManager.ViewModels;
using HL7InfobuttonAPI;

namespace ClinicalKnowledgeManager.Controllers
{
    [LogFilter]
    public class TopicsController : Controller
    {
        private readonly TopicRepository Repository = null;
        private readonly ViewModelFactory Factory;
        private readonly Parser Parser = new Parser();

        public TopicsController()
        {
            Repository = new TopicRepository();
            Factory = new ViewModelFactory(Repository);
        }

        public TopicsController(string contextName)
        {
            Repository = new TopicRepository(contextName);
            Factory = new ViewModelFactory(Repository);
        }

        //
        // GET: /Topics/

        public ActionResult Index()
        {
            return View(Factory.BuildTopicDetails(Repository.GetTopics()));
        }

        //
        // GET: /Topics/Search
        public ActionResult Search()
        {
            var result = Repository.SearchTopics(Request.QueryString);

            if (result.Count == 1)
            {
                return Redirect(Url.Action("Details", new { id = result.First().Id }) + "?" + Request.QueryString);
            }

            return View(new TopicSearchResult { Topics = Factory.BuildTopicDetails(result.ToList()) });
        }

        //
        // GET: /Topics/Details/5  or /Topics/Details/topic-alias
        public ActionResult Details(string id = "")
        {
            int topicId = 0;
            Topic topic = null;
            var queryString = new NameValueCollection();
            if (int.TryParse(id, out topicId))
            {
                topic = Repository.GetTopicById(topicId);
                queryString = Request.QueryString;
            }
            else {
                TopicAlias alias = Repository.GetTopicAliasByName(id);
                if (alias != null)
                {
                    topicId = alias.TopicId;
                    topic = alias.Topic;
                    if (!string.IsNullOrWhiteSpace(alias.Context))
                    {
                        queryString = Parser.SplitStringParameters(alias.Context);
                    }
                }
            }

            if (topic == null)
            {
                return HttpNotFound();
            }

            var result = Repository.SearchSubTopicsForTopic(topicId, queryString);

            var details = Factory.BuildTopicDetails(topic, null);
            details.ContextSubTopics = result;

            foreach (var subTopic in details.SubTopics)
            {
                FlagContextSubTopics(subTopic, result);
            }

            return View(details);
        }

        /// <summary>
        /// Recursively look at a subtopic and it's subtopics and determine if any of them should be visible because of
        /// the request context.  A few notes about how we flag items:
        ///  - Ancestors of a context item are also flagged so they are visible
        ///  - If no context sub-topics exist, everything is flagged as context
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool FlagContextSubTopics(SubTopicDetail detail, List<SubTopic> result)
        {
            detail.IsContextItem = (result.Count == 0 || result.Count(x => x.Id == detail.SubTopic.Id) > 0);

            if (detail.SubTopics != null)
            {
                bool flaggedChild = false;
                flaggedChild = detail.SubTopics.Select(subTopic => FlagContextSubTopics(subTopic, result)).Aggregate(flaggedChild, (current, recentFlag) => current || recentFlag);
                detail.IsContextItem = detail.IsContextItem || flaggedChild;
            }

            return detail.IsContextItem;
        }

    }
}