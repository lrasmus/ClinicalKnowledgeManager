using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.Tests.DB
{
    [TestClass]
    public class ContextParamsTests : TestBase
    {
        [TestMethod]
        public void NoParamsSpecified()
        {
            var parameters = new ContextParams() { InformationRecipient = "", SearchCode = "", SearchCodeSystem = "" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(5, result.Count());
        }

        [TestMethod]
        public void FindByRecipient()
        {
            var parameters = new ContextParams() { InformationRecipient = "PAT", SearchCode = "", SearchCodeSystem = "" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, result[0].Id);
            Assert.AreEqual(4, result[1].Id);

            parameters = new ContextParams() { InformationRecipient = "PROV", SearchCode = "", SearchCodeSystem = "" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);

            parameters = new ContextParams() { InformationRecipient = "NOTHING", SearchCode = "", SearchCodeSystem = "" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void FindByRecipientAndCode()
        {
            var parameters = new ContextParams() { InformationRecipient = "PAT", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);

            parameters = new ContextParams() { InformationRecipient = "PROV", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0].Id);
        }

        [TestMethod]
        public void FindBySubTopic()
        {
            var parameters = new ContextParams() { SubTopicCode = "Q000175", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);

            // If we are only searching on sub-topic and dont' find a match, we get nothing back
            parameters = new ContextParams() { SubTopicCode = "Q000174", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not sub-topic, we still return the topic result
            parameters = new ContextParams() { SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96", SubTopicCode = "Q000174", SubTopicCodeSystem = "2.16.840.1.113883.6.177" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByTask()
        {
            var parameters = new ContextParams() { Task = "MLREV" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);

            // If we are only searching on task and dont' find a match, we get nothing back
            parameters = new ContextParams() { Task = "PROBLISTREV" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not task, we still return the topic result
            parameters = new ContextParams() { Task = "NOTHING", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByGender()
        {
            var parameters = new ContextParams() { Gender = "F" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0].Id);

            // If we are only searching on gender and dont' find a match, we get nothing back
            parameters = new ContextParams() { Gender = "M" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not gender, we still return the topic result
            parameters = new ContextParams() { Gender = "M", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByAgeGroup()
        {
            var parameters = new ContextParams() { AgeGroup = "D008875" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);

            // If we are only searching on age group and dont' find a match, we get nothing back
            parameters = new ContextParams() { AgeGroup = "D000369" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not age group, we still return the topic result
            parameters = new ContextParams() { AgeGroup = "D000369", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);

            // Match on topic and age group
            parameters = new ContextParams() { AgeGroup = "D008875", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);
        }

        [TestMethod]
        public void FindByPerformerLanguage()
        {
            var parameters = new ContextParams() { PerformerLanguage = "es" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(5, result[0].Id);

            // If we are only searching on language and dont' find a match, we get nothing back
            parameters = new ContextParams() { PerformerLanguage = "fr" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not language, we still return the topic result
            parameters = new ContextParams() { PerformerLanguage = "fr", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByInformationRecipientLanguage()
        {
            var parameters = new ContextParams() { RecipientLanguage = "es" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(5, result[0].Id);

            // If we are only searching on language and dont' find a match, we get nothing back
            parameters = new ContextParams() { RecipientLanguage = "fr" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not language, we still return the topic result
            parameters = new ContextParams() { RecipientLanguage = "fr", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByBothLanguages()
        {
            // Recipient language wins out
            var parameters = new ContextParams() { PerformerLanguage = "en", RecipientLanguage = "es" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(5, result[0].Id);
        }

        [TestMethod]
        public void FindByPerformerProviderCode()
        {
            var parameters = new ContextParams() { PerformerProviderCode = "200000000X" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result[0].Id);

            // If we are only searching on provider code and dont' find a match, we get nothing back
            parameters = new ContextParams() { PerformerProviderCode = "300000000X" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not ProviderCode, we still return the topic result
            parameters = new ContextParams() { PerformerProviderCode = "300000000X", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByInformationRecipientProviderCode()
        {
            var parameters = new ContextParams() { RecipientProviderCode = "200000000X" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result[0].Id);

            // If we are only searching on provider code and dont' find a match, we get nothing back
            parameters = new ContextParams() { RecipientProviderCode = "300000000X" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not ProviderCode, we still return the topic result
            parameters = new ContextParams() { RecipientProviderCode = "300000000X", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindByBothProviderCodes()
        {
            // Recipient ProviderCode wins out
            var parameters = new ContextParams() { PerformerProviderCode = "200000000X", RecipientProviderCode = "163W00000X" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0].Id);
        }

        [TestMethod]
        public void FindByEncounter()
        {
            var parameters = new ContextParams() { EncounterCode = "AMB" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(3, result[0].Id);

            // If we are only searching on encounter and dont' find a match, we get nothing back
            parameters = new ContextParams() { EncounterCode = "VR" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(0, result.Count());

            // If we match on a topic but not encounter, we still return the topic result
            parameters = new ContextParams() { EncounterCode = "VR", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
        }

        [TestMethod]
        public void FindBestMatchWithUnusedParameters()
        {
            var parameters = new ContextParams() { InformationRecipient = "", SearchCode = "424500005", SearchCodeSystem = "2.16.840.1.113883.6.96" };
            var result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);

            parameters = new ContextParams() { InformationRecipient = "", SearchCode = "424500005", SearchCodeSystem = "" };
            result = DataContext.SearchForTopicsBasedOnContext(parameters);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[1].Id);
            Assert.AreEqual(4, result[2].Id);
        }
    }
}
