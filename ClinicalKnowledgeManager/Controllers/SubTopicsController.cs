﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using ClinicalKnowledgeManager.ViewModels;
using ClinicalKnowledgeManager.Filters;

namespace ClinicalKnowledgeManager.Controllers
{
    [LogFilter]
    public class SubTopicsController : Controller
    {
        private TopicRepository Repository = null;
        private ViewModelFactory Factory = null;

        public SubTopicsController()
        {
            Repository = new TopicRepository();
            Factory = new ViewModelFactory(Repository);
        }
        //
        // GET: /SubTopics/

        public ActionResult Index()
        {
            return View(Repository.GetSubTopics());
        }

        //
        // GET: /SubTopics/Details/5

        public ActionResult Details(int id = 0)
        {
            SubTopic subtopic = Repository.GetSubTopicById(id);
            if (subtopic == null)
            {
                return HttpNotFound();
            }

            SubTopicDetail detail = Factory.BuildSubTopicDetail(subtopic, null, 1);
            detail.IsOnlyItemDisplayed = true;
            return View(detail);
        }

        ////
        //// GET: /SubTopics/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /SubTopics/Create

        //[HttpPost]
        //public ActionResult Create(SubTopic subtopic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Context.SubTopics.Add(subtopic);
        //        Context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(subtopic);
        //}

        ////
        //// GET: /SubTopics/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    SubTopic subtopic = Context.SubTopics.Find(id);
        //    if (subtopic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subtopic);
        //}

        ////
        //// POST: /SubTopics/Edit/5

        //[HttpPost]
        //public ActionResult Edit(SubTopic subtopic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Context.Entry(subtopic).State = EntityState.Modified;
        //        Context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(subtopic);
        //}

        ////
        //// GET: /SubTopics/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    SubTopic subtopic = Context.SubTopics.Find(id);
        //    if (subtopic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subtopic);
        //}

        ////
        //// POST: /SubTopics/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SubTopic subtopic = Context.SubTopics.Find(id);
        //    Context.SubTopics.Remove(subtopic);
        //    Context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    Context.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}