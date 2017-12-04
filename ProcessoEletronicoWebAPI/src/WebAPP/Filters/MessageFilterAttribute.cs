using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP.Filters
{
    public class MessageFilterAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
        //        filterContext.ActionDescriptor.ActionName);

        //    base.OnActionExecuting(filterContext);
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{

        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Items["messages"] = null;
            base.OnActionExecuted(filterContext);
        }
    }
}
