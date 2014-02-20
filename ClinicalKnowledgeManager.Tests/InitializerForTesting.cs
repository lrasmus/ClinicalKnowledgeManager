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

            Assembly thisAssembly = Assembly.Load("ClinicalKnowledgeManager");
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            string script = (new StreamReader(thisAssembly.GetManifestResourceStream("ClinicalKnowledgeManager.DB.Scripts.Dev-Setup.sql"))).ReadToEnd();
            server.ConnectionContext.ExecuteNonQuery(script);
            conn.Close();
        }
    }
}
