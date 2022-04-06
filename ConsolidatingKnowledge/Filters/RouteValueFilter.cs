using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Filters
{
    public class RouteValueFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // have no effect on model binding because it happens before filter is executed
            context.HttpContext.Request.RouteValues["id"] = 1;
                
            //write.WriteStartObject()
            context.HttpContext.RequestServices.GetService(typeof(JsonWriter));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // override original value localhost/student/5/hung -> 5 would be replaced by 8 
            Console.WriteLine(context.HttpContext.Request.RouteValues["id"].ToString());
        }
    }
}
