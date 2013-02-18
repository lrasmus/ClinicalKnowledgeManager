using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstStoredProcedures;
using ClinicalKnowledgeManager.Models;

namespace ClinicalKnowledgeManager.DB
{
    public class SearchForTopicsBasedOnContext : IStoredProcedure<Topic>
    {
        public string InformationRecipient { get; set; }
        public string SearchCode { get; set; }
        public string SearchCodeSystem { get; set; }
    }
}