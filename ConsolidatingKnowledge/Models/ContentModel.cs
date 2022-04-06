using System.Collections.Generic;

namespace ConsolidatingKnowledge.Models
{
    public class ContentModel
    {
        public string Name { get; set; }
        public  IDictionary<string, object> Properties { get;set; }
    }
}
