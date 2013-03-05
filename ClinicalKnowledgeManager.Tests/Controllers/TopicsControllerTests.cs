using System;
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
        protected const string ContextName = "CKMDB.Test";

        [TestMethod]
        public void Index()
        {
            var controller = new TopicsController(ContextName);
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            var model = result.Model as IEnumerable<TopicDetail>;
            Assert.IsNotNull(model);
            Assert.AreEqual(4, model.Count());
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
        public void Search_ReturnMultiple()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "informationRecipient=PROV");
            var result = controller.Search() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as ViewModels.TopicSearchResult;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Topics.Count());
            Assert.AreEqual("PROV", model.ContextQuery.InformationRecipient);
        }

        [TestMethod]
        public void Details()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.96&mainSearchCriteria.v.c=424500005&informationRecipient=PROV");
            var result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as TopicDetail;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.ContextSubTopics.Count());

            SetControllerContext(controller, "informationRecipient=NOTHING");
            result = controller.Details(1) as ViewResult;
            model = result.Model as TopicDetail;
            Assert.AreEqual(0, model.ContextSubTopics.Count());
        }

        [TestMethod]
        public void Details_ContextFlags()
        {
            var controller = new TopicsController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.96&mainSearchCriteria.v.c=424500005&informationRecipient=PROV");
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as TopicDetail;
            var subTopics = model.SubTopics.ToList();
            Assert.IsFalse(subTopics[0].IsContextItem);
            Assert.IsTrue(subTopics[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[0].IsContextItem);
            Assert.IsFalse(subTopics[1].SubTopics.ToList()[1].IsContextItem);

            SetControllerContext(controller, "");
            result = controller.Details(1) as ViewResult;
            model = result.Model as TopicDetail;
            subTopics = model.SubTopics.ToList();
            Assert.IsTrue(subTopics[0].IsContextItem);
            Assert.IsTrue(subTopics[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[0].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[1].IsContextItem);
            Assert.IsTrue(subTopics[1].SubTopics.ToList()[2].IsContextItem);
        }

        private void SetControllerContext(Controller controller, string queryString)
        {
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
            var testQueryString = HttpUtility.ParseQueryString(queryString);
            request.ExpectGet(req => req.QueryString).Returns(testQueryString);
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.Expect(ctx => ctx.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()));
        }
    }
}
