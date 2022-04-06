using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.DI
{
    public class MiddlewareDI
    {
        private readonly RequestDelegate _next;
        private readonly MiddleWareDIService _service;

        public MiddlewareDI(RequestDelegate next, MiddleWareDIService service)
        {
            _next = next;
            _service = service;
        }

        public Task Invoke(HttpContext httpContext)
        {
            return _next(httpContext);
        }
    }
}
