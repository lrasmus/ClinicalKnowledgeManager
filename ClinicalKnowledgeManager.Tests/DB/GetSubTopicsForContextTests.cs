using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.Tests.DB
{
    [TestClass]
    public class GetSubTopicsForContextTests : TestBase
    {
        [TestMethod]
        public void NoParamsSpecified()
        {
            var parameters = new ContextParams() { InformationRecipient = "", SearchCode = "", SearchCodeSystem = "" };
            var result = DataContext.GetSubTopicsForContext(1, parameters);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void ParamSpecified()
        {
            var parameters = new ContextParams() { InformationRecipient = "", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            var result = DataContext.GetSubTopicsForContext(1, parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);
            Assert.AreEqual(2, result[0].ParentId);
            Assert.AreEqual("SubTopic", result[0].ParentType);
        }

        [TestMethod]
        public void ParamSpecified_SubTopic()
        {
            var parameters = new ContextParams() { SubTopicCode = "Q000628", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            var result = DataContext.GetSubTopicsForContext(1, parameters);
            Assert.AreEqual(5, result.Count());
            Assert.IsNotNull(result.Select(x => x.Id == 2).FirstOrDefault());
            Assert.IsNotNull(result.Select(x => x.ParentId == 1).FirstOrDefault());
            Assert.IsNotNull(result.Select(x => x.ParentType == "Topic").First());
        }
    }
}
