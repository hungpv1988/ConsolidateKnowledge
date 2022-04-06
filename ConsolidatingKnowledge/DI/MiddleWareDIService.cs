using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.DI
{
    public class MiddleWareDIService
    {
        public MiddleWareDIService() 
        {
            Name = DateTime.Now.ToString();
        }
        public string Name { get; set; }
    }
}
