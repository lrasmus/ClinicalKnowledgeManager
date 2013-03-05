using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicalKnowledgeManager.DB;
using CodeFirstStoredProcedures;

namespace ClinicalKnowledgeManager.Tests.DB
{
    [TestClass]
    public class GetSubTopicsForContextTests : TestBase
    {
        [TestMethod]
        public void NoParamsSpecified()
        {
            var storedProc = new GetSubTopicsForContext() { TopicID = 1, InformationRecipient = "", SearchCode = "", SearchCodeSystem = "" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void ParamSpecified()
        {
            var storedProc = new GetSubTopicsForContext() { TopicID = 1, InformationRecipient = "", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);
            Assert.AreEqual(2, result[0].ParentId);
            Assert.AreEqual("SubTopic", result[0].ParentType);
        }

        [TestMethod]
        public void ParamSpecified_SubTopic()
        {
            var storedProc = new GetSubTopicsForContext() { TopicID = 1, SubTopicCode = "Q000628", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result[0].Id);
            Assert.AreEqual(1, result[0].ParentId);
            Assert.AreEqual("Topic", result[0].ParentType);
        }
    }
}
