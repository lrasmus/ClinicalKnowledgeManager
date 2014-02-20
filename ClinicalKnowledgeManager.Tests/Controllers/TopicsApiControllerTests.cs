using System;
using System.Configuration;
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
        //protected const string ContextName = "CKMDBEntities.Test";
        protected string ContextName = "";

        [TestInitialize]
        public void Initialize()
        {
            ContextName = ConfigurationManager.ConnectionStrings["CKMDBEntities.Test"].ConnectionString;
        }

        [TestMethod]
        public void FindFirstMatchingTopic_Results()
        {
            var controller = new TopicsApiController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.177&mainSearchCriteria.v.c=Q000628&informationRecipient=PROV");
            var response = controller.FindFirstMatchingTopic();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var topic = response.Content.ReadAsAsync<TopicSearchResult>();
            Assert.AreEqual(1, topic.Result.Topics.Count());
            Assert.AreEqual(1, topic.Result.Topics.First().SubTopics.Count());
        }

        [TestMethod]
        public void FindFirstMatchingTopic_NoResults()
        {
            var controller = new TopicsApiController(ContextName);
            SetControllerContext(controller, "mainSearchCriteria.v.cs=2.16.840.1.113883.6.100&mainSearchCriteria.v.c=900");
            var response = controller.FindFirstMatchingTopic();
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        private void SetControllerContext(ApiController controller, string queryString)
        {
            var message = new HttpRequestMessage();
            message.RequestUri = new Uri("http://test/api/TopicsApi/FindFirstMatchingTopic?" + queryString);
            message.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            controller.Request = message;
        }
    }
}
