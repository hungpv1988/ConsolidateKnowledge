using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.Models
{
    public class Student
    {
        public Student() 
        {
           
        }
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }

        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

      //  [JsonExtensionData]
     //   public Dictionary<string, object> OtherInfo { get; set; }
    }
}
