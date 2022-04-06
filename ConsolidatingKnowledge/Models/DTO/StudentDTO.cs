using ConsolidatingKnowledge.ModelBinder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Models.DTO
{
    public class StudentDTO
    {
        public StudentDTO() 
        {
            OtherInfo = new Dictionary<string, object>();
        }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }
        
        public DateTime EnrollmentDate { get; set; }

        public Dictionary<string, object> OtherInfo { get; set; }
    }
}
