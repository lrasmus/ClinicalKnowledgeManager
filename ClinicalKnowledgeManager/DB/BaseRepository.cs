using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ClinicalKnowledgeManager.DB
{
    public class BaseRepository
    {
        public CKMDBEntities Context { get; set; }

        public BaseRepository()
        {
            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["CKMDB"];
            if (connection == null)
            {
                Context = new CKMDBEntities();
            }
            else
            {
                Context = new CKMDBEntities(connection.ConnectionString);
            }
        }

        public BaseRepository(string connection)
        {
            Context = new CKMDBEntities(connection);
        }
    }
}