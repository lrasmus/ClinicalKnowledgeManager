using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalKnowledgeManager.Models;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.ViewModels;
using ClinicalKnowledgeManager.Helpers;
using CodeFirstStoredProcedures;
using HL7InfobuttonAPI;

namespace ClinicalKnowledgeManager.Controllers
{
    public class TopicsController : Controller
    {
        private ModelContext Context = new ModelContext();
        private ViewModelFactory Factory = null;

        public TopicsController()
        {
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
            var parser = new Parser();
            var request = parser.ParseRequest(Request.QueryString);
            var mapper = new QueryMapper(request);

            var storedProc = new SearchForTopicsBasedOnContext()
                {
                    InformationRecipient = mapper.GetInformationRecipient(),
                    SearchCode = mapper.GetSearchCode(),
                    SearchCodeSystem = mapper.GetSearchCodeSystem()
                };
            var result = Context.Database.ExecuteStoredProcedure(storedProc);

            return View(Factory.BuildTopicDetails(result.ToList()));
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

            TopicDetail details = new TopicDetail()
                {
                    Topic = topic,
                    SubTopics = Factory.BuildSubTopicsForTopic(topic)
                };

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
    }
}