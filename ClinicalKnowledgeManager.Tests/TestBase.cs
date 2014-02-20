using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClinicalKnowledgeManager.DB;
using System.Data.Entity;

namespace ClinicalKnowledgeManager.Tests
{
    [TestClass]
    public class TestBase
    {
        protected TopicRepository DataContext;

        [TestInitialize]
        public void InitTest()
        {
            //if (Database.Exists("CKMDB.Test"))
            //{
            //    Database.Delete("CKMDB.Test");
            //}
            //Database.SetInitializer(new InitializerForTesting());

            DataContext = new TopicRepository(ConfigurationManager.ConnectionStrings["CKMDBEntities.Test"].ConnectionString);
            //DataContext.Database.Initialize(true);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            //DataContext.Dispose();
        }
    }
}
