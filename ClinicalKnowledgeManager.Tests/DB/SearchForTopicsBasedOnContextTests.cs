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
