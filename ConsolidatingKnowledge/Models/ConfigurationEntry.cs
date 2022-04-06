using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Models
{
    public class ConfigurationEntry
    {
        [Key]
        public int EntryId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
