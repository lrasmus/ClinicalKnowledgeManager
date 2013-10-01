using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.ViewModels
{
    public class ContextFilter
    {
        public IContextQuery ContextQuery { get; set; }

        public ContextFilter(IContextQuery contextQuery)
        {
            ContextQuery = contextQuery;
        }

        public string Render()
        {
            return "TEST";
        }
    }
}