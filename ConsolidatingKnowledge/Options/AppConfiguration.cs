using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Options
{
    public class AppConfiguration
    {
        public int DIType { get; set; }

        [StringLength(60, ErrorMessage = "Length cannot be longer than 60 characters")]
        public string AppName { get; set; }

        public string Secret { get; set; }
    }
}
