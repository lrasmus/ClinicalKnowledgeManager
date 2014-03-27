using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;
using IActionFilter = System.Web.Http.Filters.IActionFilter;

namespace ClinicalKnowledgeManager.Filters
{
    public class ApiLogFilter: System.Web.Http.Filters.ActionFilterAttribute, IActionFilter
    {
        private readonly TopicRepository Repository = new TopicRepository();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Repository.LogRequest(HttpContext.Current.Request.UserHostAddress,
                HttpContext.Current.Request.RawUrl);
        }
    }
}