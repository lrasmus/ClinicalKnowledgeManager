using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using ClinicalKnowledgeManager.ViewModels;

namespace ClinicalKnowledgeManager.Controllers
{
    public class TopicsController : Controller
    {
        private readonly TopicRepository Repository = null;
        private readonly ViewModelFactory Factory;

        public TopicsController()
        {
            Factory = new ViewModelFactory(Repository);
            Repository = new TopicRepository();
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

            return View(new TopicSearchResult
                {
                Topics = Factory.BuildTopicDetails(result.ToList()),
                //ContextQuery = search.StoredProcedure
                });
        }

        //
        // GET: /Topics/Details/5

        public ActionResult Details(int id = 0)
        {
            Topic topic = Repository.GetTopicById(id);
            if (topic == null)
            {
                return HttpNotFound();
            }

            var result = Repository.SearchSubTopicsForTopic(id, Request.QueryString);

            var details = new TopicDetail
                {
                    Topic = topic,
                    SubTopics = Factory.BuildSubTopicsForTopic(topic, null).ToList(),
                    ContextSubTopics = result,
                    //ContextQuery = search.StoredProcedure
                };

            foreach (var subTopic in details.SubTopics)
            {
                FlagContextSubTopics(subTopic, result);
            }

            return View(details);
        }

        ////
        //// GET: /Topics/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Topics/Create

        //[HttpPost]
        //public ActionResult Create(Topic topic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Repository.Add(topic);
        //        return RedirectToAction("Index");
        //    }

        //    return View(topic);
        //}

        ////
        //// GET: /Topics/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Topic topic = Repository.GetById(id);
        //    if (topic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(topic);
        //}

        ////
        //// POST: /Topics/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Topic topic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Repository.Update(topic);
        //        //Context.Entry(topic).State = EntityState.Modified;
        //        //Context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(topic);
        //}

        ////
        //// GET: /Topics/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Topic topic = Context.Topics.Find(id);
        //    if (topic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(topic);
        //}

        ////
        //// POST: /Topics/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Topic topic = Context.Topics.Find(id);
        //    Context.Topics.Remove(topic);
        //    Context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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