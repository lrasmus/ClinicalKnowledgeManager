using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using ClinicalKnowledgeManager.Lib;
using ClinicalKnowledgeManager.Models;
using ClinicalKnowledgeManager.ViewModels;
using CodeFirstStoredProcedures;
using HL7InfobuttonAPI;

namespace ClinicalKnowledgeManager.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ModelContext Context = new ModelContext();
        private readonly ViewModelFactory Factory;

        public TopicsController()
        {
            Factory = new ViewModelFactory(Context);
        }

        public TopicsController(string contextName)
        {
            Context = new ModelContext(contextName);
            Factory = new ViewModelFactory(Context);
        }

        //
        // GET: /Topics/

        public ActionResult Index()
        {
            return View(Factory.BuildTopicDetails(Context.Topics.ToList()));
        }

        //
        // GET: /Topics/Search
        public ActionResult Search()
        {
            var search = new TopicRequestSearch(Context);
            var result = search.SearchTopics(Request.QueryString);

            if (result.Count == 1)
            {
                return Redirect(Url.Action("Details", new { id = result.First().Id }) + "?" + Request.QueryString);
            }

            return View(new TopicSearchResult
                {
                Topics = Factory.BuildTopicDetails(result.ToList()),
                ContextQuery = search.StoredProcedure
                });
        }

        //
        // GET: /Topics/Details/5

        public ActionResult Details(int id = 0)
        {
            Topic topic = Context.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }

            var search = new TopicRequestSearch(Context);
            var result = search.SearchSubTopicsForTopic(id, Request.QueryString);

            var details = new TopicDetail
                {
                    Topic = topic,
                    SubTopics = Factory.BuildSubTopicsForTopic(topic, null).ToList(),
                    ContextSubTopics = result,
                    ContextQuery = search.StoredProcedure
                };

            foreach (var subTopic in details.SubTopics)
            {
                FlagContextSubTopics(subTopic, result);
            }

            return View(details);
        }

        //
        // GET: /Topics/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Topics/Create

        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                Context.Topics.Add(topic);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        //
        // GET: /Topics/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Topic topic = Context.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        //
        // POST: /Topics/Edit/5

        [HttpPost]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(topic).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        //
        // GET: /Topics/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Topic topic = Context.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        //
        // POST: /Topics/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = Context.Topics.Find(id);
            Context.Topics.Remove(topic);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
            base.Dispose(disposing);
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