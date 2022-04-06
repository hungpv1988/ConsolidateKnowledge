using ConsolidatingKnowledge.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsolidatingKnowledge.DI
{
    public class StudentService : IStudentService
    {
        private List<Student> _studentList;
        public StudentService() 
        {
            _studentList = new List<Student>()
            {
                new Student()
                {
                    ID = 1,
                    LastName = "Hung"
                },
                new Student()
                {
                    ID = 2,
                    LastName = "Hieu"
                },
                new Student()
                {
                    ID = 3,
                    LastName = "Hoa"
                },
                new Student()
                {
                    ID = 4,
                    LastName = "Huyen"
                },
                new Student()
                {
                    ID = 5,
                    LastName = "Hang"
                },
            };
        }
        public Student Get(int Id)
        {
            // add this to make compatible to ScopedStudentService
            if (_studentList.Any(s => s.ID == Id)) 
            {
                return _studentList.FirstOrDefault(s => s.ID == Id);
            }

            return _studentList.First();
        }
    }
}
