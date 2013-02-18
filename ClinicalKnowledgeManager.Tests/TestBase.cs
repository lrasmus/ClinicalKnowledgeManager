using System;
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
        protected ModelContext DataContext;

        [TestInitialize]
        public void InitTest()
        {
            if (Database.Exists("CKMDB.Test"))
            {
                Database.Delete("CKMDB.Test");
            }
            Database.SetInitializer(new InitializerForTesting());

            DataContext = new ModelContext("CKMDB.Test");
            DataContext.Database.Initialize(true);
        }

        [TestCleanup]
        public void CleanupTest()
        {
            DataContext.Dispose();
        }
    }
}
