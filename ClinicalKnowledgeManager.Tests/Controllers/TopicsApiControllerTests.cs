using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using ClinicalKnowledgeManager.Controllers;
using ClinicalKnowledgeManager.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClinicalKnowledgeManager.Tests.Controllers
{
    [TestClass]
    public class TopicsApiControllerTests : TestBase
    {

        protected const string ContextName = "CKMDB.Test";

        [TestMethod]
        public void FindFirstMatchingTopic_NoResults()
        {
            var controller = new TopicsApiController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.96&mainSearchCriteria.v.c=424500005&informationRecipient=PROV");
            var response = controller.FindFirstMatchingTopic();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var topic = response.Content.ReadAsAsync<TopicSearchResult>();
            Assert.AreEqual(1, topic.Result.Topics.Count());
        }

        [TestMethod]
        public void FindFirstMatchingTopic_Result()
        {
            var controller = new TopicsApiController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.100&mainSearchCriteria.v.c=900");
            var response = controller.FindFirstMatchingTopic();
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        private void SetControllerContext(ApiController controller, string queryString)
        {
            HttpRequestMessage message = new HttpRequestMessage();
            message.RequestUri = new Uri("http://test/api/TopicsApi/FindFirstMatchingTopic?" + queryString);
            message.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            controller.Request = message;
//            controller.Request = new HttpRequestMessage();
            //Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
            //var testQueryString = HttpUtility.ParseQueryString(queryString);
            //request.ExpectGet(req => req.QueryString).Returns(testQueryString);
            //Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            //context.Expect(ctx => ctx.Request).Returns(request.Object);
            //controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()));
        }
    }
}
