using ConsolidatingKnowledge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsolidatingKnowledge.DI
{
    public class TransientStudentService : IStudentService
    {
        private List<Student> _studentList;
        public TransientStudentService()
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
            var random = new Random();
            if (Id == Constants.NoStudentIdExist) 
            {
                Id = random.Next(0, _studentList.Count);
            }

            return _studentList[Id];
        }
    }
}
