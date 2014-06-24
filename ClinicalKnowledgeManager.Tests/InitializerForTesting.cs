// -----------------------------------------------------------------------
// <copyright file="InitializerForTesting.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClinicalKnowledgeManager.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClinicalKnowledgeManager.DB;
    using System.Reflection;
    using System.IO;

    [TestClass]
    public class InitializerForTesting
    {
        [AssemblyInitialize]
        public static void InitializeDatabase(TestContext context)
        {
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["CKMDB.Test"].ConnectionString;

            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            ExecuteSqlScript("ClinicalKnowledgeManager", "ClinicalKnowledgeManager.DB.Scripts.Dev-Setup.sql", server);
            ExecuteSqlScript("ClinicalKnowledgeManager.Tests", "ClinicalKnowledgeManager.Tests.Test-Setup.sql", server);
            conn.Close();
        }

        private static void ExecuteSqlScript(string assembly, string resource, Server server)
        {
            Assembly thisAssembly = Assembly.Load(assembly);
            string script = (new StreamReader(thisAssembly.GetManifestResourceStream(resource))).ReadToEnd();
            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }
}
