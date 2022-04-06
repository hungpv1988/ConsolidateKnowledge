using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Linq;


namespace ConsolidatingKnowledge.ActionConstraints
{
    public class IsMobile : Attribute, IActionConstraint
    {
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Headers["User-Agent"].Any(x => x.Contains("Android"));
        }
    }
}
