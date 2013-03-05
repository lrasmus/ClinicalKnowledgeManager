using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CodeFirstStoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.Tests.DB
{
    [TestClass]
    public class SearchForTopicsBasedOnContextTests : TestBase
    {
        [TestMethod]
        public void NoParamsSpecified()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "", SearchCode = "", SearchCodeSystem = "" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void FindByRecipient()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "PAT", SearchCode = "", SearchCodeSystem = "" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, result[0].Id);
            Assert.AreEqual(4, result[1].Id);

            storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "PROV", SearchCode = "", SearchCodeSystem = "" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);

            storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "NOTHING", SearchCode = "", SearchCodeSystem = "" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void FindByRecipientAndCode()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "PAT", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);

            storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "PROV", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0].Id);
        }

        [TestMethod]
        public void FindBySubTopic()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { SubTopicCode = "Q000175", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);

            // If we are only searching on sub-topic and dont' find a match, we get nothing back
            storedProc = new SearchForTopicsBasedOnContext() { SubTopicCode = "Q000174", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not sub-topic, we still return the topic result
            storedProc = new SearchForTopicsBasedOnContext() { SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96", SubTopicCode = "Q000174", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByTask()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { Task = "MLREV" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);

            // If we are only searching on task and dont' find a match, we get nothing back
            storedProc = new SearchForTopicsBasedOnContext() { Task = "PROBLISTREV" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not task, we still return the topic result
            storedProc = new SearchForTopicsBasedOnContext() { Task = "NOTHING", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByGender()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { Gender = "F" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0].Id);

            // If we are only searching on gender and dont' find a match, we get nothing back
            storedProc = new SearchForTopicsBasedOnContext() { Gender = "M" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not gender, we still return the topic result
            storedProc = new SearchForTopicsBasedOnContext() { Gender = "M", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByAgeGroup()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { AgeGroup = "D008875" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);

            // If we are only searching on age group and dont' find a match, we get nothing back
            storedProc = new SearchForTopicsBasedOnContext() { AgeGroup = "D000369" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not age group, we still return the topic result
            storedProc = new SearchForTopicsBasedOnContext() { AgeGroup = "D000369", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);

            // Match on topic and age group
            storedProc = new SearchForTopicsBasedOnContext() { AgeGroup = "D008875", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);
        }

        [TestMethod]
        public void FindBestMatchWithUnusedParameters()
        {
            var storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            var result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);

            storedProc = new SearchForTopicsBasedOnContext() { InformationRecipient = "", SearchCode = "424500005", SearchCodeSystem = "" };
            result = DataContext.Database.ExecuteStoredProcedure(storedProc).ToArray();
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
            Assert.AreEqual(4, result[2].Id);
        }
    }
}
