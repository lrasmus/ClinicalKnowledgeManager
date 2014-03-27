using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicalKnowledgeManager.DB;
using ClinicalKnowledgeManager.Helpers;

namespace ClinicalKnowledgeManager.Filters
{
    public class LogFilter: ActionFilterAttribute, IActionFilter
    {
        private readonly TopicRepository Repository = new TopicRepository();

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            //try
            //{
                Repository.LogRequest(filterContext.HttpContext.Request.UserHostAddress,
                    filterContext.HttpContext.Request.RawUrl);
            //}
            //catch { }
            this.OnActionExecuting(filterContext);
        }
    }
}