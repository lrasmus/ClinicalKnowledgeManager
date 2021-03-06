﻿using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicalKnowledgeManager.Controllers;
using System.Web.Mvc;
using ClinicalKnowledgeManager.ViewModels;
using Moq;
using System.Web;
using System.Web.Routing;

namespace ClinicalKnowledgeManager.Tests.Controllers
{
    [TestClass]
    public class TopicsControllerTests : TestBase
    {
        //protected const string ContextName = "CKMDBEntities.Test";
        protected string ContextName = "";
        
        [TestInitialize]
        public void Initialize()
        {
            ContextName = ConfigurationManager.ConnectionStrings["CKMDBEntities.Test"].ConnectionString;
        }

        [TestMethod]
        public void Index()
        {
            var controller = new TopicsController(ContextName);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            var model = result.Model as IEnumerable<TopicDetail>;
            Assert.IsNotNull(model);
            Assert.AreEqual(5, model.Count());
        }

        [TestMethod]
        public void Search_ReturnSingle()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.96&mainSearchCriteria.v.c=424500005&informationRecipient=PROV");
            var result = controller.Search() as RedirectResult;
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void Search_ByTerm_ReturnSingle()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.dn=clopidogrel%20metabolism");
            var result = controller.Search() as RedirectResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Url.Contains("mainSearchCriteria.v.dn=clopidogrel%20metabolism"));
        }

        [TestMethod]
        public void Search_ReturnMultiple()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "informationRecipient=PROV");
            var result = controller.Search() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as ViewModels.TopicSearchResult;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Topics.Count());
            //Assert.AreEqual("PROV", model.ContextQuery.InformationRecipient);
        }

        [TestMethod]
        public void Details_NoneExist()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "");
            var result = controller.Details("") as ViewResult;
            Assert.IsNull(result);

            SetControllerContext(controller, "");
            result = controller.Details("99999") as ViewResult;
            Assert.IsNull(result);

            SetControllerContext(controller, "");
            result = controller.Details("invalid-alias") as ViewResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Details()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.96&mainSearchCriteria.v.c=424500005&informationRecipient=PROV");
            var result = controller.Details("1") as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as TopicDetail;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.ContextSubTopics.Count());

            // If there are no matching sub-topics (which there won't be in this case), we want to get all sub-topics back.
            SetControllerContext(controller, "informationRecipient=NOTHING");
            result = controller.Details("1") as ViewResult;
            model = result.Model as TopicDetail;
            Assert.AreEqual(6, model.ContextSubTopics.Count());
        }

        [TestMethod]
        public void Details_ContextFlags()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.96&mainSearchCriteria.v.c=424500005&informationRecipient=PROV");
            var result = controller.Details("1") as ViewResult;
            var model = result.Model as TopicDetail;
            var subTopics = model.SubTopics.ToList();
            Assert.IsFalse(subTopics[0].IsContextItem);
            Assert.IsTrue(subTopics[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[0].IsContextItem);
            Assert.IsFalse(subTopics[1].SubTopics.ToList()[1].IsContextItem);

            SetControllerContext(controller, "");
            result = controller.Details("1") as ViewResult;
            model = result.Model as TopicDetail;
            subTopics = model.SubTopics.ToList();
            Assert.IsTrue(subTopics[0].IsContextItem);
            Assert.IsTrue(subTopics[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[0].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[2].IsContextItem);
        }

        [TestMethod]
        public void Details_Alias()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "");
            var result = controller.Details("clopidogrel-physician") as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as TopicDetail;
            Assert.AreEqual(1, model.Topic.Id);
            var subTopics = model.SubTopics.ToList();
            Assert.IsTrue(subTopics[0].IsContextItem);
            Assert.IsTrue(subTopics[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[0].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[1].IsContextItem);

            SetControllerContext(controller, "");
            result = controller.Details("clopidogrel-poor-metabolizer-physician") as ViewResult;
            Assert.IsNotNull(result);
            model = result.Model as TopicDetail;
            Assert.AreEqual(1, model.Topic.Id);
            subTopics = model.SubTopics.ToList();
            Assert.IsFalse(subTopics[0].IsContextItem);
            Assert.IsTrue(subTopics[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[0].IsContextItem);
            Assert.IsFalse(subTopics[1].SubTopics.ToList()[1].IsContextItem);
        }

        [TestMethod]
        public void Details_Alias_MultipleResults()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "");
            var result = controller.Details("clopidogrel") as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as TopicSearchResult;
            Assert.AreEqual(2, model.Topics.Count());
            Assert.AreEqual("Search", result.ViewName);
        }

        private void SetControllerContext(Controller controller, string queryString)
        {
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
            var testQueryString = HttpUtility.ParseQueryString(queryString);
            request.SetupGet(req => req.QueryString).Returns(testQueryString);
            request.SetupGet(req => req.RawUrl).Returns("http://test/" + (string.IsNullOrWhiteSpace(queryString) ? string.Empty : "?" + queryString));
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()));
        }
    }
}
